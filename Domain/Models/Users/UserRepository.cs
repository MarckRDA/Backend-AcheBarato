using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Infra;
using Domain.Models.Users;

namespace Domain.src.Users
{
    public class UserRepository : IUserRepository
    {
        public void AddUser(User user)
        {
            using (var _context = new AcheBaratoContext())
            {
                _context.Add(user);
                _context.SaveChanges();
            }

        }

        public IEnumerable<User> GetUsers()
        {
            using (var _context = new AcheBaratoContext())
            {
                return _context.Users.ToList();
            }
        }

        public User GetUser(Guid idUser)
        {
            using (var _context = new AcheBaratoContext())
            {
                return _context.Users.FirstOrDefault(u => u.UserId == idUser);
            }
        }

        public void RemoveUser(Guid idUser)
        {
            using (var _context = new AcheBaratoContext())
            {
                var usuarioARemover = _context.Users.FirstOrDefault(u => u.UserId == idUser);
                _context.Users.Remove(usuarioARemover);
                _context.SaveChanges();
            }

        }

    }
}