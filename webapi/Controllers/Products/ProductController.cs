using System;
using System.Linq;
using Domain.Models.Products;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers.Products
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
        //[EnableQuery]
        public IQueryable<Product> GetSearch([FromQuery]ProductQueryParameters parameters)
        {
            return _productServices.GetAllProduct(parameters).OrderBy(x => x.Price); 
        }

        [HttpGet("{idMLBProduct}")]
        public IActionResult GetProducyById(Guid idMLBProduct)
        {
            return Ok(_productServices.GetProductDTOById(idMLBProduct));
        }

        [HttpGet("cathegories")]
        public IActionResult GetCathegories()
        {
            return Ok(_productServices.GetCathegories());
        }


        [HttpGet("cathegories/{category}")]
        public IActionResult GetCathegories(string category)
        {
            return Ok(_productServices.GetProductsByCategory(category));
        }
        
    }
}