using Domain.Models.Products;
using MongoDB.Bson.Serialization;

namespace Domain.Infra.Mapping
{
    public class ProductMap
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<Product>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
                map.MapIdMember(x => x.Id);
                map.MapMember(x => x.Name).SetIsRequired(true);
                map.MapMember(x => x.ProductIdMLB).SetIsRequired(true);
                map.MapMember(x => x.Price).SetIsRequired(true);
                map.MapMember(x => x.ThumbImgLink).SetIsRequired(true);
                map.MapMember(x => x.Cathegory).SetIsRequired(true);
                map.MapMember(x => x.Descriptions).SetIsRequired(true);
                map.MapMember(x => x.HistorycalṔrices).SetIsRequired(true);
                map.MapMember(x => x.Pictures).SetIsRequired(true);
            });
        }
    }
}