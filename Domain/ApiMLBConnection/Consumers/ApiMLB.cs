using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Domain.ApiMLBConnection.Instance;
using Domain.ApiMLBConnection.Interfaces;
using Domain.Models.Cathegories;
using Domain.Models.Descriptions;
using Domain.Models.HistorycalPrices;
using Domain.Models.Products;
using Newtonsoft.Json.Linq;

namespace Domain.ApiMLBConnection.Consumers
{
    public class ApiMLB : IApi
    {
        public static string BaseUrl
        {
            get
            {
                return "https://api.mercadolibre.com";
            }
        }

        public static string[] TrendSearchesInML { get; set; } = new string[]
        {
            "MLB1648",
            "MLB1051",
            "MLB5726",
            "MLB1144",
            "MLB1196"
        };


        public static List<Product> GetProducts(string productSearch)
        {
            string action = BaseUrl + $"/sites/MLB/search?q={productSearch}";

            JArray products = (JArray)GetMethodHandler(action)["results"];

            return GetBestSellers(products);
        }

        public static List<List<Product>> GetTrendsProducts()
        {
            var products = new List<List<Product>>();
            foreach (var trend in TrendSearchesInML)
            {

                string action = BaseUrl + $"/trends/MLB/{trend}";

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, action);

                HttpResponseMessage response = HttpInstance.GetHttpClientInstance().SendAsync(request).Result;

                var keywordProductForSearch = JArray.Parse(response.Content.ReadAsStringAsync().Result)[0]["keyword"];

                products.Add(GetProducts(keywordProductForSearch.ToString()).Take(10).ToList());
            }

            return products;
        }

        public static List<Product> GetProductsByCathegory(string cathegoryId)
        {
            string action = $"/sites/MLB/search?category={cathegoryId}";

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, BaseUrl + action);

            HttpResponseMessage response = HttpInstance.GetHttpClientInstance().SendAsync(request).Result;

            JArray product = (JArray)JObject.Parse(response.Content.ReadAsStringAsync().Result)["results"];

            return GetBestSellers(product);
        }

        public static JObject GetProductByMLBId(string idMLB)
        {
            string action = BaseUrl + $"/items/{idMLB}";
            return GetMethodHandler(action);
        }


        public static double FindWhetherProductPriceChanges(string idMLB)
        {
            return double.Parse(GetProductByMLBId(idMLB)["price"].ToString());
        }

        public static List<List<Product>> PutProductsInBackGround()
        {
            string action = BaseUrl + $"/sites/MLB/categories";

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, action);

            HttpResponseMessage response = HttpInstance.GetHttpClientInstance().SendAsync(request).Result;

            var product = JArray.Parse(response.Content.ReadAsStringAsync().Result);
            var listProducts = new List<List<Product>>();

            for (int index = 0; index < product.Count; index++)
            {
                var productsToPutDB = GetProductsByCathegory(product[index]["id"].ToString());
                listProducts.Add(productsToPutDB);
            }

            return listProducts;
        }

        private static string GetCathegoriesChildrendById(string cathegoryId)
        {
            string action = BaseUrl + $"/categories/{cathegoryId}";

            var cathegories = GetMethodHandler(action);

            return cathegories["name"].ToString();
        }


        private static List<Product> GetBestSellers(JArray json2Filter)
        {
            var productsWithTrustedSellers = new List<Product>();

            foreach (var pd in json2Filter)
            {
                try
                {
                    if (pd["seller"]["seller_reputation"]["power_seller_status"].ToString() == "platinum" || pd["seller"]["seller_reputation"]["power_seller_status"].ToString() == "gold")
                    {
                        var createTag = pd["title"].ToString().ToUpper();

                        var productFullInformation = GetProductByMLBId(pd["id"].ToString());

                        var titleProduct = productFullInformation["title"].ToString();

                        var idMLBProduct = productFullInformation["id"].ToString();

                        var cathegoryMLB = productFullInformation["category_id"].ToString();

                        var priceProduct = productFullInformation["price"].ToString();

                        var redirLink = productFullInformation["permalink"].ToString();

                        var thumbnailPic = productFullInformation["thumbnail"].ToString();

                        var pics = productFullInformation["pictures"];

                        var listDescriptions = productFullInformation["attributes"];

                        var listDescriptionsObject = new List<Description>();

                        foreach (var description in listDescriptions)
                        {
                            createTag += " " + description["value_name"].ToString().ToUpper();
                            listDescriptionsObject.Add(new Description(description["name"].ToString(),
                                description["value_name"].ToString()));
                        }

                        var categoryName = GetCathegoriesChildrendById(cathegoryMLB);
                        
                        createTag += " " + categoryName;
                        
                        var getProductFromAPIToDB = new Product(titleProduct,
                            idMLBProduct,
                            double.Parse(priceProduct),
                            thumbnailPic,
                            redirLink,
                            new Cathegory(cathegoryMLB, categoryName),
                            createTag.Split(' '));


                        foreach (var description in listDescriptionsObject)
                        {
                            getProductFromAPIToDB.AddDescription(description);
                        }

                        getProductFromAPIToDB.AddHistoricalPrice(new HistorycalPrice(double.Parse(priceProduct), DateTime.Now.ToShortDateString()));

                        foreach (var pic in pics)
                        {
                            getProductFromAPIToDB.AddPicture(pic["url"].ToString());
                        }

                        productsWithTrustedSellers.Add(getProductFromAPIToDB);
                    }
                }
                catch (System.Exception)
                {
                    continue;
                }


            }

            return productsWithTrustedSellers;
        }

        private static JObject GetMethodHandler(string endpoint)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, endpoint);

            HttpResponseMessage response = HttpInstance.GetHttpClientInstance().SendAsync(request).Result;

            return JObject.Parse(response.Content.ReadAsStringAsync().Result);
        }
    }
}