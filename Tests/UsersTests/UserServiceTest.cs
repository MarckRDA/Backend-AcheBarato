using Domain.Models.Users;
using Moq;
using Xunit;

namespace Tests.Users
{
    public class UserServiceTest
    {
        private Mock<IUserRepository> _usersRepository;
        private UserService userService;

        public UserServiceTest()
        {
            _usersRepository = new Mock<IUserRepository>();
            userService = new UserService(_usersRepository.Object);

        }

        public User GenerateUserClient()
        {
            return new User("Rodrigo", "Rodrigo.boy@gmail.com", "senha", Profile.Client, "47991320566");
        }

        public User GenerateUserAdm()
        {
            return new User("Rodrigo", "Rodrigo.boy@gmail.com", "senha", Profile.Adm, "47991320566");
        }

        [Fact]
        public void AddUser_is_invalid()
        {
            //Given
            var validation = userService.CreateUser("Rodrigo alecrim dourado", "Rodrigo.boy@gmail.com", "senha", Profile.Adm, "47991320566");

            //When
            _usersRepository.Verify(x => x.add(It.IsAny<User>()), Times.Never());

            //Then
            Assert.Null(validation);
        }

        [Fact]
        public void AddUser_is_valid()
        {
            //Given
            var validation = userService.CreateUser("Matheus Tallmann", "senha", "matheus.delas7787@gmail.com", Profile.Adm, "47991320566");

            //When
            _usersRepository.Verify(x => x.add(It.IsAny<User>()), Times.Once());
            
            //Then
            Assert.NotNull(validation);
        }

        [Fact]
        public void TestName()
        {
            //Given
            var user = userService.CreateUser("Matheus Tallmann", "senha", "matheus.delas7787@gmail.com", Profile.Adm, "47991320566");
            var mock = new Mock<IUserService>();

            //When
            _usersRepository.Setup(x => x.GetEntityById(x => x.Id, user.Id)).Returns(user);
            var returnedUser = userService.GetUserById(user.Id);
            mock.Setup(x => x.GetUserById(user.Id)).Returns(user);
            // mock.Object
            
            //Then
            Assert.Equal(user, returnedUser);
        }
    }
}