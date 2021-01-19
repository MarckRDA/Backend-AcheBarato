using System;
using System.Linq;
using Domain.Models.Products;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers.Products
{
    [ApiController]

    [Route("achebarato/[Controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductServices _productServices;

        public ProductsController(IProductServices services)
        {
            _productServices = services;
        }

        [HttpGet]
        public IQueryable<Product> GetSearch([FromQuery] ProductQueryParameters parameters)
        {
            return _productServices.GetAllProduct(parameters).OrderBy(x => x.Price).Take(10);
        }

        [HttpGet("trendproducts")]
        public IActionResult GetTrendProducts()
        {
            return Ok(_productServices.GetTrendProductsDTO());
        }

        [HttpGet("{idProduct}")]
        public IActionResult GetProducyById(Guid idProduct)
        {
            return Ok(_productServices.GetProductDTOById(idProduct));
        }

        [HttpGet("categories")]
        public IActionResult GetCathegories()
        {
            return Ok(_productServices.GetCathegories());
        }

        [HttpGet("{idProduct}/descriptions")]
        public IActionResult GetProductDescriptions(Guid idProduct)
        {
            return Ok(_productServices.GetProductDTOById(idProduct).Descriptions);
        }

        [HttpGet("categories/{category}")]
        public IActionResult GetCathegories(string category)
        {
            return Ok(_productServices.GetProductsByCategory(category));
        }

    }
}