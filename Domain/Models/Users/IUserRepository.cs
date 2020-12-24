using System;
using Domain.Infra;

namespace Domain.Models.Users
{
    public interface IUserRepository : IRepository<User>
    {
        User GetUserById(Guid user);
    }
}