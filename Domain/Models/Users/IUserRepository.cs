using System;
using System.Collections.Generic;


namespace Domain.Models.Users
{
    public interface IUserRepository
    {
        void AddUser(User user);
        IEnumerable<User> GetUsers();
        User GetUser(Guid idUser);
        void RemoveUser(Guid idUser);        
    }
}