using System;
using System.Collections.Generic;
using Domain.Models.AlarmPrices;
using Domain.Models.Cathegories;
using Domain.Models.Products;
using Domain.Models.Users;
using Xunit;

namespace Tests.Users
{
    public class UserDTOTest
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public List<Product> WishListProducts {get; set;} = new List<Product>();
        public List<string> SearchTag { get; set; } = new List<string>();
        public List<AlarmPrice> WishProductsAlarmPrices {get; set;} = new List<AlarmPrice>();
        
        private Product ProductGenerator()
        {
            return new Product(
                "Aves", "MLA1100", 100, "Link", "Redirect", new Cathegory("MLA5725", "Accesorios para Vehiculos"), new string[1]{"tag"}
            );
        }
        // [Fact]
        // public void TestName()
        // {
        //     //Given, When
        //     var userDTO = new UserDTO();
        //     userDTO.Name = "Marcos Rocha";
        //     userDTO.SearchTag.Add("job");
        //     userDTO.UserId = Guid.NewGuid();
        //     userDTO.WishListProducts.Add(ProductGenerator());
        //     userDTO.WishProductsAlarmPrices.Add(new AlarmPrice(Guid.NewGuid(), 11.42));
            
        //     //Then
        // }
    }    
}