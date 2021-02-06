using Domain.Models.Users;
using Xunit;

namespace Tests
{
    public class UsersTests
    {
        [Fact]
        public void Should_Create_An_User()
        {
            var test = new User("Marcos Alves", "marcos@gmail.com", "12345", Profile.Client, "+5592897128341");
            
            Assert.NotNull(test);
        }
    }
}
