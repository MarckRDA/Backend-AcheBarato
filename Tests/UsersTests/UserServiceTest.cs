using System;
using Domain.Models.AlarmPrices;
using Domain.Models.Users;
using Moq;
using Tests.Mocks;
using Xunit;

namespace Tests.Users
{
    public class UserServiceTest : MyMocks
    {
        [Fact]
        public void AddUser_is_invalid()
        {
            
            //Given
            var validation = UserService.CreateUser("Vinicius oliveira", "Vinicius.oliveira@gmail.com", "senha", Profile.Adm, "47991320566");

            //When
            UsersRepository.Verify(x => x.add(It.IsAny<User>()), Times.Never());

            //Then
            Assert.Null(validation);
        }

        [Fact]
        public void CreateUser_is_valid()
        {
            //Given
            var validation = UserService.CreateUser("Matheus Tallmann", "senha", 
                "matheus.tallmann7787@gmail.com", Profile.Adm, "47991320566"
            );

            //When
            UsersRepository.Verify(x => x.add(It.IsAny<User>()), Times.Once());
            
            //Then
            Assert.NotNull(validation);
        }

        [Fact]
        public void AddUser_is_valid()
        {
            //Given
            var user = UserService.CreateUser("Matheus Tallmann", "senha", "matheus.tallmann7787@gmail.com", Profile.Adm, "47991320566");

            //When
            UsersRepository.Verify(x => x.add(It.Is<User>(x => 
                x.Name == "Matheus Tallmann" &&
                x.Password == "senha" && 
                x.Email == "matheus.tallmann7787@gmail.com" &&
                x.Profile == Profile.Adm &&
                x.PhoneNumber ==  "47991320566"
            )), Times.Once());
            

            //Then
            Assert.NotNull(user);
        }

        [Fact]
        public void GetUserById_valid()
        {
            //Given
            var user = UserService.CreateUser("Matheus Tallmann", "senha", 
                "matheus.tallmann7787@gmail.com", Profile.Adm, "47991320566"
            );

            //Then
            UserService.GetUserById(user.Id);
            
            // assert
            UsersRepository.Verify(x => 
                x.GetEntityById(x => x.Id, user.Id), 
                Times.Once()
            );
        }

        [Fact]
        public void GetUserByEmail_valid()
        {
            //Given
            var user = UserService.CreateUser("Matheus Tallmann", "senha", 
                "matheus.tallmann7787@gmail.com", Profile.Adm, "47991320566"
            );

            //Then
            UserService.GetUserByEmail("matheus.tallmann7787@gmail.com");
            
            // assert
            UsersRepository.Verify(x => 
                x.GetUserByEmail("matheus.tallmann7787@gmail.com"), 
                Times.Once()
            );
        }

        [Fact]
        public void AddSearchTagInUserPreferences_valid()
        {
            //Given
            var user = UserService.CreateUser("Matheus Tallmann", "senha", 
                "matheus.tallmann7787@gmail.com", Profile.Adm, "47991320566"
            );
            
            //When
            user.AddAlarmPrice(new AlarmPrice(Guid.NewGuid(), 18));
            UsersRepository.Object.UpdateUserInformations(user);
            
            //Then
            UsersRepository.Verify(x => x.UpdateUserInformations(user), Times.Once());
        }

        [Fact]
        public void UpdateAlarmPriceProductInformations_valid()
        {
            //Given
            var user = UserService.CreateUser("Matheus Tallmann", "senha", 
                "matheus.tallmann7787@gmail.com", Profile.Adm, "47991320566"
            );
            
            //When
            user.AddTagSearch("string");
            UsersRepository.Object.UpdateUserInformations(user);
            
            //Then
            UsersRepository.Verify(x => x.UpdateUserInformations(user), Times.Once());
        }        
    }
}