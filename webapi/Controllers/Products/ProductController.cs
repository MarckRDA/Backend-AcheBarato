using System;
using System.Linq;
using Domain.Common;
using Domain.Models.Products;
using Microsoft.AspNetCore.Mvc;
using webapi.Services.URIBuilder;
using webapi.Services.Wrappers;

namespace webapi.Controllers.Products
{
    [ApiController]

    [Route("achebarato/[Controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductServices _productServices;
        private readonly IURIService _uriSevice;

        public ProductsController(IProductServices services, IURIService uriService)
        {
            _productServices = services;
            _uriSevice = uriService;
        }

        [HttpGet]
        public IActionResult GetSearch([FromQuery] QueryParameters parameters)
        {
            var searchQuery = String.Join("", parameters.Search.Split(" "));
            var route = $"{Request.Path.Value}?search={searchQuery}";
            var products = _productServices.GetAllProduct(parameters);
            
            var totalQuantity = products.quantityData;
                
            return Ok(PaginationHelper.CreatePagedReponse<ProductDTO>(products.productsSeached.ToList(), parameters, totalQuantity, _uriSevice, route));
        }

        [HttpGet("{idProduct}")]
        public IActionResult GetProducyById(Guid idProduct)
        {
            return Ok(new Response<ProductDTO>(_productServices.GetProductDTOById(idProduct)));
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