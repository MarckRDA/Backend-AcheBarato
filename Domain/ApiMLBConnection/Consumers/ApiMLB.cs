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

            var products = GetMethodHandler(action);

            return GetBestSellers((JArray)products["results"]);

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
            string action = BaseUrl + $"/sites/MLB/search?category={cathegoryId}";

            JArray products = (JArray)GetMethodHandler(action)["results"];

            return GetBestSellers(products);
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