using System;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Product
{
    [ApiController]

    [Route("achebarato/[Controller]")]
    public class ProductsController : Controller
    {
        private readonly IProductServices _productServices;

        public ProductsController(IProductServices services)
        {
            _productServices = services;
        }

        [HttpGet]
        public IActionResult GetSearch(string q)
        {
            var products = _productServices.GetAllProduct(q); 
            return Ok(products);
        }

        [HttpGet("{idMLBProduct}")]
        public IActionResult GetProducyById(Guid idMLBProduct)
        {
            return Ok(_productServices.GetProductDTOById(idMLBProduct));
        }

        
        
    }
}