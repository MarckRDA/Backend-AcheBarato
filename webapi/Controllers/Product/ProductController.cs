using Domain.Models.Products;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Product
{
    [ApiController]

    [Route("achebarato/[Controller]")]
    public class ProductController : Controller
    {
        private readonly IProductServices _productServices;

        public ProductController(IProductServices services)
        {
            _productServices = services;
        }

        [HttpGet]
        public IActionResult GetSearch(string q)
        {
            var searching = this.Request.QueryString.ToString();
            return Ok(_productServices.GetAllProduct(searching));
        }
    }
}