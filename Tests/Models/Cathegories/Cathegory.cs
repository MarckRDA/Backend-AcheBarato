using System.Collections.Generic;
using Domain.Models.Cathegories;
using Xunit;

namespace Tests.Cathegories
{
    public class CathegoryTest
    {
        [Fact]
        public void Create_Cathegory()
        {
            //Given, When
            var cathegory = new Cathegory("idMLB", "name");
            
            //Then
            Assert.Equal("name", cathegory.Name);
            Assert.Equal("idMLB", cathegory.IdMLB);
            Assert.NotNull(cathegory);
        }
    }
}