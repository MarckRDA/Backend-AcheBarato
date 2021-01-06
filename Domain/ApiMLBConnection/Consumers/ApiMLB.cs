using System.Collections.Generic;
using System.Net.Http;
using Domain.ApiMLBConnection.Instance;
using Domain.ApiMLBConnection.Interfaces;
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


        private static JObject GetProductByMLBId(string idMLB)
        {
            string action = BaseUrl + $"/items/{idMLB}";
            return GetMethodHandler(action);
        }



        private static string GetCathegoriesChildrendById(string cathegoryId)
        {
            string action = BaseUrl + $"/categories/{cathegoryId}";

            var cathegories = GetMethodHandler(action);

            return cathegories["name"].ToString();
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
                    var createTag = pd["title"].ToString();

                    var tagList = new List<Tag>();

                    var tagsSplited = createTag.Split(' ');

                    foreach (var tag in tagsSplited)
                    {
                        tagList.Add(new Tag() { Name = tag.ToUpper() });
                    }

                    var productFullInformation = GetProductByMLBId(pd["id"].ToString());

                    var titleProduct = productFullInformation["title"].ToString();

                    var idMLBProduct = productFullInformation["id"].ToString();

                    var cathegoryMLB = productFullInformation["category_id"].ToString();

                    var priceProduct = productFullInformation["price"].ToString();

                    var redirLink = productFullInformation["permalink"].ToString();

                    var thumbnailPic = productFullInformation["thumbnail"].ToString();

                    var listPicture = new List<Picture>();

                    var pics = productFullInformation["pictures"];

                    foreach (var pic in pics)
                    {
                        listPicture.Add(new Picture() { LinkPicture = pic["url"].ToString() });
                    }

                    var listDescriptions = productFullInformation["attributes"];
                    var listDescriptionsObject = new List<Description>();

                    foreach (var description in listDescriptions)
                    {
                        if (description["name"].ToString() == "Marca")
                        {
                            tagList.Add(new Tag() { Name = description["value_name"].ToString().ToUpper() });
                        }

                        listDescriptionsObject.Add(new Description(description["name"].ToString(),
                                                                 description["value_name"].ToString()));
                    }

                    productsWithTrustedSellers.Add(new Product()
                    {
                        Name = titleProduct,
                        ProductIdMLB = idMLBProduct,
                        Price = double.Parse(priceProduct),
                        ThumbImgLink = thumbnailPic,
                        LinkRedirectShop = redirLink,
                        Cathegory = GetCathegoriesChildrendById(cathegoryMLB),
                        Pictures = listPicture,
                        Descriptions = listDescriptionsObject,
                        Tags = tagList
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