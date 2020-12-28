using System.Collections.Generic;
using System.Net.Http;
using Domain.ApiMLBConnection.Instance;
using Domain.ApiMLBConnection.Interfaces;
using Domain.Models.Cathegories;
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

        public static List<Product> GetProducts(string productSearch)
        {
            string action = BaseUrl + $"/sites/MLB/search?q={productSearch}";

            JArray products = (JArray)GetMethodHandler(action)["results"];

            return GetBestSellers(products);

        }

        //I'm gonna change that signature.... Let me thing over that
        private static JObject GetProductByMLBId(string idMLB)
        {
            string action = BaseUrl + $"/items/{idMLB}";
            return GetMethodHandler(action);
        }

        public static List<Cathegory> GetCathegories()
        {
            string action = BaseUrl + "/sites/MLB/categories";

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, action);

            HttpResponseMessage response = HttpInstance.GetHttpClientInstance().SendAsync(request).Result;

            JArray cathegories = JArray.Parse(response.Content.ReadAsStringAsync().Result);

            var list = new List<Cathegory>();

            foreach (var item in cathegories)
            {
                list.Add(new Cathegory(item["id"].ToString())
                            {
                                NameMLB = item["name"].ToString()
                            });
            }

            return list;

        }

        public static List<CathegoryChild> GetCathegoriesChildrendById(string cathegoryId)
        {
            string action = BaseUrl + $"/categories/{cathegoryId}";

            var cathegories = (JArray)GetMethodHandler(action)["children_categories"];

            var cathegorieIn = new List<CathegoryChild>();

            foreach (var cathegory in cathegories)
            {
                //cathegorieIn.Add(cathegory.ToString());
            }

            return cathegorieIn;
        }

        public static List<Product> GetProductsByCathegory(string cathegoryId)
        {
            string action = $"/sites/MLB/search?category={cathegoryId}";

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, BaseUrl + action);

            HttpResponseMessage response = HttpInstance.GetHttpClientInstance().SendAsync(request).Result;

            JArray product = (JArray)JObject.Parse(response.Content.ReadAsStringAsync().Result)["results"];

            return GetBestSellers(product);
        }

        private static List<Product> GetBestSellers(JArray json2Filter)
        {
            var productsWithTrustedSellers = new List<Product>();

            foreach (var pd in json2Filter)
            {
                if (pd["seller"]["seller_reputation"]["power_seller_status"].ToString() == "platinum")
                {
                    var createTag = pd["title"].ToString().Split(",");
                    
                    foreach (var tag in createTag)
                    {
                        tag.ToUpper();
                    }
                    
                    var productFullInformation = GetProductByMLBId(pd["id"].ToString());
                    var  titleProduct = productFullInformation["title"].ToString();
                    var idMLBProduct = productFullInformation["id"].ToString();
                    var cathegoryMLB = productFullInformation["category_id"].ToString();
                    var priceProduct = productFullInformation["price"].ToString();
                    var redirLink = productFullInformation["permalink"].ToString();
                    var thumbnailPic = productFullInformation["thumbnail"].ToString();
                    var listPicture = new List<string>();
                    var pics = productFullInformation["pictures"];

                    foreach (var pic in pics)
                    {
                        listPicture.Add(pic["url"].ToString());
                    }

                    var listDescriptions = productFullInformation["attributes"];
                    var listDescriptionsObject = new List<Description>();

                    foreach (var description in listDescriptions)
                    {
                        listDescriptionsObject.Add(new Description(description["id"].ToString(), 
                                                                description["name"].ToString(),
                                                                 description["value_name"].ToString()));
                    }

                    productsWithTrustedSellers.Add(new Product()
                                                    {
                                                        Name = titleProduct,
                                                        ProductIdMLB = idMLBProduct,
                                                        Price = double.Parse(priceProduct),
                                                        ThumbImgLink = thumbnailPic,
                                                        LinkRedirectShop = redirLink,
                                                        Cathegory = new Cathegory(cathegoryMLB),
                                                        Pictures = listPicture,
                                                        Descriptions = listDescriptionsObject,
                                                        Tags = createTag
                                                    });
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