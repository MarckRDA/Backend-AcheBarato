using System;

namespace Domain.Models.Users
{
    public interface IUserService
    {
        User CreateUser(string name, string password, string email, Profile profile, string phoneNumber) ;
        User GetUser(Guid idUser);
        (bool isValid, Guid id) AddUser(string name, string password, string email, Profile profile, string phoneNumber);
        User GetUserByEmail(string userEmail);
        bool UpdateAlarmPriceProductInformations(Guid userId, Guid productId, double priceToMonitor);
    }
}