using Domain.Models.Cathegories;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult GetCathegoryChildren(string idCathgory)
        {
            return Ok(_cathegoryService.GetCathegoryChildren(idCathgory));
        }

    }
}