using System;
using System.Linq;
using System.Threading.Tasks;
using Domain.ApiMLBConnection.Consumers;
using Domain.Models.HistorycalPrices;
using Domain.Models.Products;
using Domain.Models.SenderEntities;
using Domain.Models.Users;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using webapi.Services.MessagerBrokers;

namespace webapi.Services.BackgroundService
{
    public class ProductBackgroundTask : IProductBackgroundTask
    {
        private readonly IMongoCollection<Product> _collectionProducts;
        private readonly IMongoCollection<User> _collectionUsers;
        private readonly IMessagerBroker _messagerBroker;
        private readonly IConfiguration _configuration;
        public ProductBackgroundTask(IMessagerBroker messagerBroker, IConfiguration configuration)
        {
            _messagerBroker = messagerBroker;
            _configuration = configuration;
            var client = new MongoClient(_configuration.GetValue<string>("MongoSettings:Connection"));
            var database = client.GetDatabase(_configuration.GetValue<string>("MongoSettings:DatabaseName"));
            _collectionProducts = database.GetCollection<Product>("Products");
            _collectionUsers = database.GetCollection<User>("Users");
        }

        public void PushProductsInDB()
        {
            try
            {
                var listProductsToPush = ApiMLB.PutProductsInBackGround();

                var filter = Builders<Product>.Filter;

                foreach (var productList in listProductsToPush)
                {
                    _collectionProducts.InsertManyAsync(productList);
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

        public Task MonitorPriceProducts()
        {
            var productsInDB = _collectionProducts.AsQueryable().ToList();

            foreach (var product in productsInDB)
            {
                try
                {
                    var price = ApiMLB.FindWhetherProductPriceChanges(product.MLBId);
                    product.UpdateProductPrice(price);
                    product.AddHistoricalPrice(new HistorycalPrice(price, DateTime.Now.ToShortDateString()));
                    _collectionProducts.ReplaceOne(
                    p => p.id_product == product.id_product,
                    product
                );

                }
                catch (System.Exception ex)
                {
                    var filterDeleteProduct = Builders<Product>.Filter.Eq(product => product.id_product, product.id_product);
                    _collectionProducts.DeleteOne(filterDeleteProduct);
                    throw new Exception($"Error in monitor price {ex.Message}");
                }

            }

            return Task.CompletedTask;
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
                    if (alarm.IsTheSamePrice(productInMonitoring.Price))
                    {
                        var linkRedirect = $"https://localhost:3000/ProdutoEscolhido/{productInMonitoring.id_product}";

                        _messagerBroker.SendEntityToNotify(new SenderEntity(
                                                            user.Name,
                                                            user.Email,
                                                            user.PhoneNumber,
                                                            productInMonitoring.Name,
                                                            productInMonitoring.Price,
                                                            productInMonitoring.ThumbImgLink,
                                                            linkRedirect
                        ));
                    }

                }
            }
        }
    }
}