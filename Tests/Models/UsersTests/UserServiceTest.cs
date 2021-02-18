using System;
using Domain.Models.AlarmPrices;
using Domain.Models.Users;
using Moq;
using Xunit;

namespace Tests.Users
{
    public class UserServiceTest : UsersTestMethods
    {
        [Fact]
        public void CreateUser_ExistAnyUserWithThisEmail_return_true()
        {
            //Given
            UsersRepository.SetupAllProperties();
            UsersRepository
                .Setup(u => u.ExistAnyUserWithThisEmail(It.IsAny<string>()))
                .Returns(true);

            //When
            var validation = UserService.CreateUser("Vinicius oliveira", "senha", "Vinicius.oliveira@gmail.com", Profile.Adm, "47991320566");

            //Then
            UsersRepository.Verify(x => x.add(It.IsAny<User>()), Times.Never());
            UsersRepository.Verify(x => x.ExistAnyUserWithThisEmail(It.IsAny<string>()), Times.Once());
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
            UsersRepository.Verify(x => x.add(It.Is<User>(x => 
                x.Name == "Matheus Tallmann" &&
                x.Password == "senha" && 
                x.Email == "matheus.tallmann7787@gmail.com" &&
                x.Profile == Profile.Adm &&
                x.PhoneNumber ==  "47991320566"
            )), Times.Once());

            //Then
            Assert.NotNull(validation);
        }

        [Fact]
        public void CreateUser_user_is_invalid()
        {
            //Given, When
            var validation = UserService.CreateUser("Matheus Tallmann", "senha", 
                "", Profile.Adm, "47991320566"
            );

            //Then
            UsersRepository.Verify(x => x.add(It.IsAny<User>()), Times.Never());
            Assert.Null(validation);
        }

        [Fact]
        public void GetUserById_valid()
        {
            //Given
            var user = UserService.CreateUser("Matheus Tallmann", "senha", 
                "matheus.tallmann7787@gmail.com", Profile.Adm, "47991320566"
            );
            UsersRepository.Setup(r => r.GetEntityById(x => x.Id, user.Id)).Returns(user);

            //Then
            var returnedUser = UserService.GetUserById(user.Id);
            
            // assert
            UsersRepository.Verify(x => x.GetEntityById(x => x.Id, user.Id),Times.Once());
            Assert.NotNull(returnedUser);
        }

        [Fact]
        public void GetUserById_is_invalid()
        {
            //Given
            var user = UserService.CreateUser("Matheus Tallmann", "senha", 
                "matheus.tallmann7787@gmail.com", Profile.Adm, "47991320566"
            );

            //Then
            var returnedUser = UserService.GetUserById(user.Id);
            
            // assert
            UsersRepository.Verify(x => x.GetEntityById(x => x.Id, user.Id),Times.Once());
            Assert.Null(returnedUser);
        }

        [Fact]
        public void GetUserByEmail_valid()
        {
            //Given
            var user = UserService.CreateUser("Matheus Tallmann", "senha", 
                "matheus.tallmann7787@gmail.com", Profile.Adm, "47991320566"
            );
            UsersRepository.Setup(u => u.GetUserByEmail("matheus.tallmann7787@gmail.com")).Returns(user);

            //Then
            var returnedUser = UserService.GetUserByEmail("matheus.tallmann7787@gmail.com");
            
            // assert
            UsersRepository.Verify(x => x.GetUserByEmail("matheus.tallmann7787@gmail.com"),Times.Once());
            Assert.NotNull(returnedUser);
        }

        [Fact]
        public void GetUserByEmail_invalid()
        {
            //Given
            var user = UserService.CreateUser("Matheus Tallmann", "senha", 
                "matheus.tallmann7787@gmail.com", Profile.Adm, "47991320566"
            );

            //Then
            var returnedUser = UserService.GetUserByEmail("matheus.tallmann7787@gmail.com");
            
            // assert
            UsersRepository.Verify(x => x.GetUserByEmail("matheus.tallmann7787@gmail.com"),Times.Once());
            Assert.Null(returnedUser);
        }        

        [Fact]
        public void AddSearchTagInUserPreferences_didnt_find_user()
        {
            //Given
            var user = UserService.CreateUser("Matheus Tallmann", "senha", 
                "matheus.tallmann7787@gmail.com", Profile.Adm, "47991320566"
            );
            
            //When
            var actual = UserService.AddSearchTagInUserPreferences(user.Id, "");
            
            //Then
            UsersRepository.Verify(x => x.GetEntityById(x => x.Id, user.Id), Times.Once());
            Assert.False(actual);
        }

        [Fact]
        public void AddSearchTagInUserPreferences_is_valid()
        {
            //Given
            var user = UserService.CreateUser("Matheus Tallmann", "senha", 
                "matheus.tallmann7787@gmail.com", Profile.Adm, "47991320566"
            );
            UsersRepository.Setup(x => x.GetEntityById(x => x.Id, user.Id)).Returns(user);
            
            //When
            var actual = UserService.AddSearchTagInUserPreferences(user.Id, "");
            var searchTagscounter = user.SearchTags.Count;
            
            //Then
            UsersRepository.Verify(x => x.GetEntityById(x => x.Id, user.Id), Times.Once());
            UsersRepository.Verify(x => x.UpdateUserInformations(user), Times.Once());
            Assert.Equal(1, searchTagscounter);
            Assert.True(actual);
        }

        [Fact]
        public void UpdateAlarmPriceProductInformations_is_valid()
        {
            //Given
            var user = CreateUserGenerator();
            UsersRepository.Setup(x => x.GetEntityById(x => x.Id, user.Id)).Returns(user);
            
            //When
            var actual = UserService.UpdateAlarmPriceProductInformations(user.Id, Guid.NewGuid(), It.IsAny<double>());
            var wishProductsAlarmPricesCount = user.WishProductsAlarmPrices.Count;
            
            //Then
            UsersRepository.Verify(x => x.GetEntityById(x => x.Id, user.Id), Times.Once());
            UsersRepository.Verify(x => x.UpdateUserInformations(user), Times.Once());
            Assert.Equal(1, wishProductsAlarmPricesCount);
            Assert.True(actual);
        }    

        [Fact]
        public void UpdateAlarmPriceProductInformations_didnt_find_user()
        {
            //Given
            var user = CreateUserGenerator();
            
            //When
            var actual = UserService.UpdateAlarmPriceProductInformations(user.Id, Guid.NewGuid(), It.IsAny<double>());
            
            //Then
            UsersRepository.Verify(x => x.GetEntityById(x => x.Id, user.Id), Times.Once());
            Assert.False(actual);
        }            
    }
}