using Microsoft.AspNetCore.Mvc;
using Domain.Models.Cathegories;

namespace webapi.Controllers.Cathegory
{
    [ApiController]
    [Route("achebarato/[Controlller]")]
    public class CathegoryController : Controller
    {
        private readonly ICathegoryService _cathegoryService;
        
        [HttpGet]
        public IActionResult GetAllCathegories()
        {
            return Ok(_cathegoryService.GetCathegories());
        }

        [HttpGet("{idCathgory}")]
        public IActionResult GetCathegoryChildren(string idCathegory)
        {
            return Ok(_cathegoryService.GetCathegoryChildren(idCathegory));
        }

    }
}