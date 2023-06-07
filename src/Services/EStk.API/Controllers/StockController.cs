using EStk.API.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EStk.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class StockController : ControllerBase
    {
        public StockController() { }

        [HttpGet]
        [Authorize(Roles = ("Admin,User"))]
        public IActionResult Get()
        {
            return Ok(
                new List<Item> {
                    new Item { Id = 1, Name = "Item 1", Price = 100 },
                    new Item { Id = 2, Name = "Item 2", Price = 200 },
                    new Item { Id = 3, Name = "Item 3", Price = 300 }
                }
            );
        }

        [HttpPost]
        [Authorize(Roles = ("Admin"))]
        public IActionResult Post()
        {
            return Ok("Created");
        }

        [HttpPut]
        [Authorize(Roles = ("Admin,User"))]
        public IActionResult Put()
        {
            return Ok("Edited");
        }
    }
}
