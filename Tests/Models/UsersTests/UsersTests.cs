using Domain.Models.Users;
using Xunit;

namespace Tests.Users
{
    public class UserTests
    {
        [Fact]
        public void Should_Create_An_User()
        {
            var test = new User("Marcos Alves", "marcos@gmail.com", "12345", Profile.Client, "+5592897128341");
            var validation = test.Validate();
            
            Assert.True(validation.isValid);
        }

        [Fact]
        public void Create_User()
        {
            var user = new User("Marcos Alves", "12345", "marcos@gmail.com", "47984956815");
            var validation = user.Validate();
            
            Assert.NotNull(user);
        }
    }
}
