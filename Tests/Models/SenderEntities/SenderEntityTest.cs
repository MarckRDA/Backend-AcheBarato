using Domain.Models.SenderEntities;
using Xunit;

namespace Tests.SenderEntities
{
    public class SenderEntityTest
    {
        [Fact]
        public void Create_SenderEntity()
        {
            //Given, When
            var senderEntity = new SenderEntity(
                "Vinicius de Oliveira", 
                "vinicius.oliveira7787@gmail.com", 
                "47992188787", 
                "pc gamer", 
                12.999, 
                "img thumb", 
                "Link Redirect"
            );
            
            //Then
            Assert.NotNull(senderEntity);
        }
    }
}