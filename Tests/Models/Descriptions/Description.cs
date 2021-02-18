using Domain.Models.Descriptions;
using Xunit;

namespace Tests.Descriptions
{
    public class DescriptionTest
    {
        [Fact]
        public void Create_Description()
        {
            //Given, When
            var description = new Description("name", "value");
            
            //Then
            Assert.Equal("name", description.Name);
            Assert.Equal("value", description.Value);
            Assert.NotNull(description);
        }
    }
}