using System.Collections.Generic;
using Domain.Common;
using Domain.Models.Cathegories;
using Domain.Models.Products;
using Domain.Models.Users;
using Moq;
using Tests.Mocks;

namespace Tests.Users
{
    public abstract class UsersTestMethods : MyMocks
    {
        protected User CreateUserGenerator()
        {
            return UserService.CreateUser("Matheus Tallmann", "senha", 
                "matheus.tallmann7787@gmail.com", Profile.Adm, "47991320566"
            );
        }
    }
}