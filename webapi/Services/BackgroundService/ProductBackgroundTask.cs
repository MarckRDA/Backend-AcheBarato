using System;
using Domain.ApiMLBConnection.Consumers;
using Domain.Models.HistorycalPrices;
using Domain.Models.Products;
using MongoDB.Driver;

namespace webapi.Services.BackgroundService
{
    public class ProductBackgroundTask
    {
        private readonly IMongoCollection<Product> _collection;

        public ProductBackgroundTask()
        {
            var database = new MongoClient("mongodb://root:AcheBaratoMongoDB2021!@localhost:27017").GetDatabase("AcheBarato");
            _collection = database.GetCollection<Product>("Products");
        }

        public void PushProductsInDB()
        {
            try
            {
                var productsToPush = ApiMLB.PutProductsInBackGround();

                foreach (var product in productsToPush)
                {
                    _collection.InsertManyAsync(product);
                }
            }
            catch (System.Exception e)
            {
                throw new System.Exception($"erro: {e.Message}");
            }

        }

        public void PushTrendProductsInDB()
        {
             try
            {
                var productsToPush = ApiMLB.GetTrendsProducts();

                foreach (var products in productsToPush)
                {
                    foreach (var product in products)
                    {
                        product.isTrending = true;
                    }
                    _collection.InsertManyAsync(products);
                }
            }
            catch (System.Exception e)
            {
                throw new System.Exception($"erro: {e.Message}");
            }
        }

        public void CleanTrendsProducts()
        {
            var findTrendProductsInDay = Builders<Product>.Filter.Eq(pd => pd.isTrending, true);
            var productsToCleanTrends = _collection.Find(findTrendProductsInDay).ToList();
            foreach (var product in productsToCleanTrends)
            {
                product.isTrending = false;
                _collection.ReplaceOne(p => p.id_product == product.id_product, product);
            }

        }

        public void MonitorPriceProducts()
        {
            var productsInDB = _collection.AsQueryable().ToList();

            foreach (var product in productsInDB)
            {
                var price = ApiMLB.FindWhetherProductPriceChanges(product.MLBId);
                product.UpdateProductPrice(price);
                product.AddHistoricalPrice(new HistorycalPrice(price, DateTime.Now));
                try
                {
                    _collection.ReplaceOneAsync(
                    p => p.id_product == product.id_product,
                    product
                );    
                }
                catch (System.Exception ex)
                {
                    
                    throw new Exception($"Error in monitor price {ex.Message}");
                }
                
            }
        }
    }
}