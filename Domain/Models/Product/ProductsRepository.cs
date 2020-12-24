using System.Collections.Generic;
using System.Linq;
using Domain.Models.Products;

namespace Domain.src.Times
{
    public static class ProductsRepository
    {
        private static List<Product> products = new List<Product>();

        public static void AddProducts(List<Product> products)
        {
            foreach (Product item in products)
            {
                products.Add(item);
            }
            
        }
        
        public static Product GetProduct(string idMlProduct)
        {
            return products.FirstOrDefault(t => t.ProductIdMLB == idMlProduct);
        }

        public static void AddProduct(Product insertProduct)
        {
            products.Add(insertProduct);
        }

        public static void RemoveProduct(string idMlProduct)
        {
            var productToRemove = products.FirstOrDefault(t => t.ProductIdMLB == idMlProduct);
            products.Remove(productToRemove);
        }

    }
}