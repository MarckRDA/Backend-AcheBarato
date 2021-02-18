using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Models.Cathegories;
using Domain.Models.Descriptions;
using Domain.Models.HistorycalPrices;
using Domain.Models.Products;
using Xunit;

namespace Tests.Products 
{
    public class ProductDTOTest : ProductsTestMethods
    {
        // [Theory]
        // [InlineData(
        //     "ce7bdcdf-f840-41a4-a9b6-c68e5f690ab7",
        //     "MLB1648",
        //     "playstation 4",
        //     2799.00,
        //     "ThumbImgLink",
        //     "LinkRedirectShop",
        //     new string[1]{"Pictures"},
        //     new string[1]{"tag"},
        //     true
        // )]
        // public void Create_ProductDTO(
        //     string idProduct,
        //     string mlbId,
        //     string name,
        //     double price,
        //     string thumbImgLink,
        //     string linkRedirectShop,
        //     string[] pictures,
        //     string[] tag,
        //     bool isTrending
        // )
        // {
        //     //Given, When
        //     // var guidGenerator = Guid.NewGuid();
        //     var productDTO = new ProductDTO();
        //     productDTO.id_product = Guid.Parse(idProduct);
        //     productDTO.MLBId = mlbId;
        //     productDTO.Name = name;
        //     productDTO.Price = price;
        //     productDTO.ThumbImgLink = thumbImgLink;
        //     productDTO.LinkRedirectShop = linkRedirectShop;
        //     productDTO.Cathegory = CathegoryGenerator();
        //     productDTO.Descriptions.Add(new Description("name", "value"));
        //     productDTO.HistorycalṔrices.Add(new HistorycalPrice(11.28, "18/01/2021"));
        //     productDTO.Pictures.AddRange(pictures.ToList());
        //     productDTO.Tag = tag;
        //     productDTO.isTrending = isTrending;
            
        //     //Then
        //     Assert.NotNull(productDTO);
        // }

        [Fact]
        public void TestName()
        {
            //Given
            // var sla = new ProductDTO(){
            //     Name = "product.Name",
            //     ThumbImgLink = "product.ThumbImgLink",
            //     Price = 2799.00,
            //     id_product = Guid.NewGuid()
            // };
            
            //When

            
            // Assert.NotNull(sla);
            //Then
        }
    }
}