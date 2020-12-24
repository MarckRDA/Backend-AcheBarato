using System.Collections.Generic;
using System.Net.Http;
using Domain.ApiMLBConnection.Instance;
using Domain.ApiMLBConnection.Interfaces;
using Newtonsoft.Json.Linq;

namespace Domain.ApiMLBConnection.Consumers
{
    public class ApiMLB : IApi
    {
        public string BaseUrl
        {
            get
            {
                return "https://api.mercadolibre.com";
            }
        }

        public List<string> GetProducts(string productSearch)
        {
            string action = BaseUrl + $"/sites/MLB/search?q={productSearch}";

            JArray products = (JArray)GetMethodHandler(action)["results"];

            return GetBestSellers(products);

        }

        //I'm gonna change that signature.... Let me thing over that
        public void GetProductByMLBId(string idMLB)
        {
            string action = BaseUrl + $"/items/{idMLB}";
            var product = GetMethodHandler(action);
        }

        public List<string> GetCathegories()
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

        public List<string> GetCathegoriesChildrendById(string cathegoryId)
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

        public List<string> GetProductsByCathegory(string cathegoryId)
        {
            string action = $"/sites/MLB/search?category={cathegoryId}";

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, BaseUrl + action);

            HttpResponseMessage response = HttpInstance.GetHttpClientInstance().SendAsync(request).Result;

            JArray product = (JArray)JObject.Parse(response.Content.ReadAsStringAsync().Result)["results"];

            return GetBestSellers(product);
        }

        private List<string> GetBestSellers(JArray json2Filter)
        {
            var productsWithTrustedSellers = new List<string>();

            foreach (var pd in json2Filter)
            {
                if (pd["seller"]["seller_reputation"]["power_seller_status"].ToString() == "platinum")
                {
                    productsWithTrustedSellers.Add(pd.ToString());
                }
            }

            return productsWithTrustedSellers;
        }

        private JObject GetMethodHandler(string endpoint)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, endpoint);

            HttpResponseMessage response = HttpInstance.GetHttpClientInstance().SendAsync(request).Result;

            return JObject.Parse(response.Content.ReadAsStringAsync().Result);

        }

    }
}