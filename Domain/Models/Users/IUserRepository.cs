using Domain.Common;

namespace Domain.Models.Users
{
    public interface IUserRepository : IMongoRepository<User>
    {
        void UpdateUserInformations(User userToUpdate);
        User GetUserByEmail(string userEmail);
        bool ExistAnyUserWithThisEmail(string email);

    }

}