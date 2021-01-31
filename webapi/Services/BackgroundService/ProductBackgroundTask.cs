using System;
using System.Linq;
using Domain.ApiMLBConnection.Consumers;
using Domain.Models.HistorycalPrices;
using Domain.Models.Products;
using Domain.Models.Users;
using MongoDB.Driver;

namespace webapi.Services.BackgroundService
{
    public class ProductBackgroundTask
    {
        private readonly IMongoCollection<Product> _collectionProducts;
        private readonly IMongoCollection<User> _collectionUsers;

        public ProductBackgroundTask()
        {
            var database = new MongoClient("mongodb://root:AcheBaratoMongoDB2021!@localhost:27017").GetDatabase("AcheBarato");
            _collectionProducts = database.GetCollection<Product>("Products");
            _collectionUsers = database.GetCollection<User>("Users");
        }

        public void PushProductsInDB()
        {
            try
            {
                var productsToPush = ApiMLB.PutProductsInBackGround();

                foreach (var product in productsToPush)
                {
                    _collectionProducts.InsertManyAsync(product);
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
                    _collectionProducts.InsertManyAsync(products);
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
            var productsToCleanTrends = _collectionProducts.Find(findTrendProductsInDay).ToList();
            foreach (var product in productsToCleanTrends)
            {
                product.isTrending = false;
                _collectionProducts.ReplaceOne(p => p.id_product == product.id_product, product);
            }

        }

        public void MonitorPriceProducts()
        {
            var productsInDB = _collectionProducts.AsQueryable().ToList();

            foreach (var product in productsInDB)
            {
                try
                {
                    var price = ApiMLB.FindWhetherProductPriceChanges(product.MLBId);
                    product.UpdateProductPrice(price);
                    product.AddHistoricalPrice(new HistorycalPrice(price, DateTime.Now.ToShortDateString()));
                    _collectionProducts.ReplaceOneAsync(
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

        public void NotifyUserAboutAlarmPrice()
        {
            var allUsers = _collectionUsers.AsQueryable().ToList();
            foreach (var user in allUsers)
            {
                var alarmsSetByUser = user.WishProductsAlarmPrices;
                if (alarmsSetByUser.Count == 0) continue;
                foreach (var alarm in alarmsSetByUser)
                {
                    var filterProduct = Builders<Product>.Filter.Eq(x => x.id_product, alarm.ProductToMonitorId);
                    var productInMonitoring = _collectionProducts.Find(filterProduct).FirstOrDefault();
                    if (productInMonitoring == null) continue;
                    //if (alarm.IsTheSamePrice(productInMonitoring.Price)) SendNotificationEmail(user.Name, productInMonitoring)

                }
            }
        }
    }
}