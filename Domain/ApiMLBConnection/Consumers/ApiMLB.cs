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
        public static void GetProductByMLBId(string idMLB)
        {
            string action = BaseUrl + $"/items/{idMLB}";
            var product = GetMethodHandler(action);
        }

        public static List<string> GetCathegories()
        {
            string action = BaseUrl + "/sites/MLB/categories";

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, action);

            HttpResponseMessage response = HttpInstance.GetHttpClientInstance().SendAsync(request).Result;

            JArray cathegories = JArray.Parse(response.Content.ReadAsStringAsync().Result);

            var list = new List<string>();

            foreach (var item in cathegories)
            {
                list.Add(item.ToString());
            }

            return list;

        }

        public static List<string> GetCathegoriesChildrendById(string cathegoryId)
        {
            string action = BaseUrl + $"/categories/{cathegoryId}";

            var cathegories = (JArray)GetMethodHandler(action)["children_categories"];

            var cathegorieInString = new List<string>();

            foreach (var cathegory in cathegories)
            {
                cathegorieInString.Add(cathegory.ToString());
            }

            return cathegorieInString;
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
                    
                    productsWithTrustedSellers.Add(new Product(pd["title"].ToString(), 
                                                    pd["id"].ToString(),
                                                    double.Parse(pd["price"].ToString()),
                                                    new Cathegory(pd["category_id"].ToString(), 
                                                    pd["domain_id"].ToString()))
                                                    {
                                                        ImgLink = pd["thumbnail"].ToString(),
                                                        LinkRedirectShop = pd["permalink"].ToString(),
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