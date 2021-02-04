using System;
using Domain.Models.AlarmPrices;

namespace Domain.Models.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;    
        }

        public (bool isValid, Guid id) AddUser(string name, string password, string email,Profile profile, string celphone)
        {
            var newUSer = CreateUser(name, password, email,profile,celphone);

            if (newUSer == null)
            {
                return (false, Guid.Empty);
            }

            return (true, newUSer.Id);       
            
             }

        public User CreateUser(string name, string password, string email,Profile profile,string celphone)
        {
            {

            var newUser = new User(name, password, email,celphone);
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

        public bool UpdateAlarmPriceProductInformations(Guid userId,  Guid productId, double priceToMonitor)
        {
            var userToUpdateAlarmPrice = GetUser(userId);
            if(userToUpdateAlarmPrice == null) return false;
            userToUpdateAlarmPrice.AddAlarmPrice(new AlarmPrice(productId, priceToMonitor));
            _repository.UpdateUserInformations(userToUpdateAlarmPrice);
            return true;
        }
    }
}