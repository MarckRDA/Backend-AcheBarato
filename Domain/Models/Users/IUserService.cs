using System;
using Domain.Models.Products;

namespace Domain.Models.Users
{
    public interface IUserService
    {
        User CreateUser(string name, string password, string email, Profile profile);
        User GetUser(Guid idUser);
        (bool isValid, Guid id) AddUser(string name, string password, string emai, Profile profile);
        User GetUserByEmail(string userEmail);
        void  UpdateAlarmPriceProductInformations(Guid userId, Product products, double priceToMonitor);
    }
}