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
        public IQueryable<ProductDTO> GetSearch([FromQuery] ProductQueryParameters parameters)
        {
            return _productServices.GetAllProduct(parameters);
        }

        [HttpGet("{idProduct}")]
        public IActionResult GetProducyById(Guid idProduct)
        {
            return Ok(_productServices.GetProductDTOById(idProduct));
        }

        [HttpGet("trendproducts")]
        public IActionResult GetTrendProducts()
        {
            return Ok(_productServices.GetTrendProductsDTO());
        }

        [HttpGet("usersPreferences/{search}")]
        public IActionResult GetProductsByUserPreferences(string search)
        {
            return Ok(_productServices.GetProdutsBasedOnUserSearches(search));
        }

        [HttpGet("categories")]
        public IActionResult GetCathegories()
        {
            return Ok(_productServices.GetCathegories());
        }

        [HttpGet("{id_product}/relatedproducts")]
        public IActionResult GetRelatedProducts(Guid id_product)
        {
            return Ok(_productServices.GetRelatedProductsDTO(id_product));
        }

        [HttpGet("{idProduct}/descriptions")]
        public IActionResult GetProductDescriptions(Guid idProduct)
        {
            return Ok(_productServices.GetProductDTOById(idProduct).Descriptions);
        }

    }
}