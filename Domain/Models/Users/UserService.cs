using System;
using Domain.Models.AlarmPrices;
using Domain.Models.Products;

namespace Domain.Models.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;    
        }

        public (bool isValid, Guid id) AddUser(string name, string password, string email,Profile profile )
        {
            var newUSer = CreateUser(name, password, email,profile);

            if (newUSer == null)
            {
                return (false, Guid.Empty);
            }

            return (true, newUSer.Id);       
            
             }

        public User CreateUser(string name, string password, string email,Profile profile)
        {
            {

            var newUser = new User(name, password, email);
            if (newUser.Validate().isValid)
            {
                _repository.add(newUser);
                return newUser;
            }

            return null;
        }

        }

        public User GetUser(Guid idUser)
        {
            return _repository.GetEntityById(x => x.Id, idUser);
        }

        public User GetUserByEmail(string userEmail)
        {
            return _repository.GetUserByEmail(userEmail);
        }

        public void UpdateAlarmPriceProductInformations(Guid userId,  Product products, double priceToMonitor)
        {
            var userToUpdateAlarmPrice = GetUser(userId);
            userToUpdateAlarmPrice.AddAlarmPrice(new AlarmPrice(products, priceToMonitor));
            _repository.UpdateUserInformations(userToUpdateAlarmPrice);
        }
    }
}