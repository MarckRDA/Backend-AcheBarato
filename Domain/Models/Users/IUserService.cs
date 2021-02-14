using System;

namespace Domain.Models.Users
{
    public interface IUserService
    {
        User CreateUser(string name, string password, string email, Profile profile, string phoneNumber) ;
        User GetUserById(Guid idUser);
        User GetUserByEmail(string userEmail);
        bool UpdateAlarmPriceProductInformations(Guid userId, Guid productId, double priceToMonitor);
        bool AddSearchTagInUserPreferences(Guid userId, string searchTag);
    }
}