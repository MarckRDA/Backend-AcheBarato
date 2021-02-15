using Xunit;
using Moq;
using Tests.Mocks;
using Domain.Models.Users;
using System.Collections.Generic;

namespace Tests.Users
{
    public class UsersRepositoryTest : MyMocks
    {
        [Fact]
        public void Add_is_valid()
        {
            //Given
            var user = UserService.CreateUser("Matheus Tallmann", "senha", 
                "matheus.tallmann7787@gmail.com", Profile.Adm, "47991320566"
            );
            
            //Then
            UsersRepository.Verify(x => x.add(user), Times.Once());
            UsersRepository.Verify(x => x.add(It.Is<User>(x => 
                x.Name == "Matheus Tallmann" &&
                x.Password == "senha" && 
                x.Email == "matheus.tallmann7787@gmail.com" &&
                x.Profile == Profile.Adm &&
                x.PhoneNumber ==  "47991320566"
            )), Times.Once());
        }

        [Fact]
        public void GetAllElements_is_valid()
        {
            //Given
            var user = UserService.CreateUser("Matheus Tallmann", "senha", 
                "matheus.tallmann7787@gmail.com", Profile.Adm, "47991320566"
            );
            var elements = UsersRepository.Object.GetAllElements();
            
            //Then
            UsersRepository.Setup(x => x.GetAllElements()).Returns(elements);
            UsersRepository.Verify(x => x.GetAllElements(), Times.Once());
            UsersRepository.Verify(x => x.add(It.Is<User>(x => 
                x.Name == "Matheus Tallmann" &&
                x.Password == "senha" && 
                x.Email == "matheus.tallmann7787@gmail.com" &&
                x.Profile == Profile.Adm &&
                x.PhoneNumber ==  "47991320566"
            )), Times.Once());
        }

        [Fact]
        public void GetUserByEmail_is_valid()
        {
            //Given
            var user = UserService.CreateUser("Matheus Tallmann", "senha", 
                "matheus.tallmann7787@gmail.com", Profile.Adm, "47991320566"
            );
            UsersRepository.Setup(x => x.GetUserByEmail(user.Email)).Returns(user);
        
            //When
            UsersRepository.Object.GetUserByEmail(user.Email);

            //Then
            UsersRepository.Verify(x => x.GetUserByEmail(user.Email), Times.Once());
        }
    }
}