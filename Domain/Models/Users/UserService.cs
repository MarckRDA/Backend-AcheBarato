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

        public User CreateUser(string name, string password, string email, Profile profile, string phoneNumber)
        {
            if (_repository.ExistAnyUserWithThisEmail(email))
            {
                return null;
            }
            
            var newUser = new User(name, email, password, profile, phoneNumber);
            if (newUser.Validate().isValid)
            {
                _repository.add(newUser);
                return newUser;
            }

            return null;
        }

        public User GetUserById(Guid idUser)
        {
            return _repository.GetEntityById(x => x.Id, idUser);
        }

        public User GetUserByEmail(string userEmail)
        {
            return _repository.GetUserByEmail(userEmail);
        }

        public bool AddSearchTagInUserPreferences(Guid userId, string searchTag)
        {
            var userToAddPreferences = GetUserById(userId);
            if (userToAddPreferences == null) return false;
            userToAddPreferences.AddTagSearch(searchTag);
            _repository.UpdateUserInformations(userToAddPreferences);
            return true;
        }

        public bool UpdateAlarmPriceProductInformations(Guid userId, Guid productId, double priceToMonitor)
        {
            var userToUpdateAlarmPrice = GetUserById(userId);
            if (userToUpdateAlarmPrice == null) return false;
            userToUpdateAlarmPrice.AddAlarmPrice(new AlarmPrice(productId, priceToMonitor));
            _repository.UpdateUserInformations(userToUpdateAlarmPrice);
            return true;
        }
    }
}