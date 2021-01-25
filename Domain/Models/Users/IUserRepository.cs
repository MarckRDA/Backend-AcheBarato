using Domain.Common;

namespace Domain.Models.Users
{
    public interface IUserRepository : IMongoRepository<User>
    {
        User GetUserByEmail(string userEmail);
    }

}