using Domain.Models.Users;
using MongoDB.Bson.Serialization;

namespace Infra.Mapping
{
    public class UserMap
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<User>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
                map.MapIdMember(x => x.Id);
                map.MapMember(x => x.Name).SetIsRequired(true);
                map.MapMember(x => x.Email).SetIsRequired(true);
                map.MapMember(x => x.PhoneNumber).SetIsRequired(true);
                map.MapMember(x => x.SearchTags).SetIsRequired(true);
                map.MapMember(x => x.Password).SetIsRequired(true);
                map.MapMember(x => x.WishListProducts).SetIsRequired(true);
                map.MapMember(x => x.WishProductsAlarmPrices).SetIsRequired(true);

            });
        }
    }
}