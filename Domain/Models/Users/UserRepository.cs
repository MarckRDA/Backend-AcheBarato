using System;
using System.Linq;
using Domain.Infra;
using Domain.Models.Users;

namespace Domain.src.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly IRepository<User> _userRepository;

        public UserRepository(IRepository<User> repository)
        {
            _userRepository = repository;
        }
      
        public void add(User user)
        {
            _userRepository.add(user);
        }

        public User GetElement(Func<User, bool> predicate)
        {
            return _userRepository.GetElement(predicate);
        }

        public User GetUserById(Guid idUser)
        {
            using (var _context = new AcheBaratoContext())
            {
                return _context.Users.FirstOrDefault(u => u.Id == idUser);
            }
        }

    }
}