using EStk.API.Data;
using EStk.API.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EStk.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class StockController : ControllerBase
    {
        public static List<Item> StockItems = new List<Item> {
                    new Item { Id = 1, Name = "Item 1", Price = 100, StockId = 1 },
                    new Item { Id = 2, Name = "Item 2", Price = 200, StockId = 1 },
                    new Item { Id = 3, Name = "Item 3", Price = 300, StockId = 2 },
                    new Item { Id = 4, Name = "Item 4", Price = 400, StockId = 2 },
                    new Item { Id = 5, Name = "Item 5", Price = 500, StockId = 3 },
                    new Item { Id = 6, Name = "Item 6", Price = 600, StockId = 3 },
                    new Item { Id = 7, Name = "Item 7", Price = 700, StockId = 1 },
                    new Item { Id = 8, Name = "Item 8", Price = 800, StockId = 1 },
                    new Item { Id = 9, Name = "Item 9", Price = 900, StockId = 2 }
                };

        const string ETAG_HEADER = "ETag";
        const string MATCH_HEADER = "If-Match";

        public StockController() { }

        [HttpGet]
        [Authorize(Roles = ("Admin,User,Auditor"))]
        public IActionResult Get()
        {
            return Ok(StockItems);
        }

        [HttpPost]
        [Authorize(Roles = ("Admin"))]
        public IActionResult Post(Item item)
        {
            var maxId = StockItems.Max(x => x.Id);
            item.Id = ++maxId;
            StockItems.Add(item);
            return Ok(StockItems);
        }

        [HttpPut]
        [Authorize(Roles = ("Admin,User"))]
        public IActionResult Put(Item item)
        {
            if (item == null || item.Id <= 0) return BadRequest();
            var stckItem = StockItems.FirstOrDefault(x => x.Id == item.Id);
            if(stckItem == null) return NotFound();
            stckItem.Name = item.Name;
            stckItem.Price = item.Price;
            stckItem.StockId = item.StockId;
            return Ok(StockItems);
        }

        [HttpGet("stock")]
        [AllowAnonymous]
        public IActionResult GetStock(int stockId)
        {
            var stockItems = StockItems.Where(x => x.StockId == stockId).ToList();
            var stock = new Stock { Id = stockId, Items = stockItems };
            var eTag = HashFactory.GetHash(stock);
            HttpContext.Response.Headers.Add(ETAG_HEADER, eTag);
            if (HttpContext.Request.Headers.ContainsKey(MATCH_HEADER) &&
              HttpContext.Request.Headers[MATCH_HEADER].RemoveQuotes() == eTag)
                return new StatusCodeResult(StatusCodes.Status304NotModified);
            return Ok(stock);
        }

        [HttpPut("stock")]
        [AllowAnonymous]
        public IActionResult IssueStock(int stockId, Stock stock)
        {
            var stockItems = StockItems.Where(x => x.StockId == stockId).ToList();
            var stockFromSaved = new Stock { Id = stockId, Items = stockItems };
            var eTag = HashFactory.GetHash(stockFromSaved);
            if (!HttpContext.Request.Headers.ContainsKey(MATCH_HEADER) ||
              HttpContext.Request.Headers[MATCH_HEADER].RemoveQuotes() != eTag)
            {
                return new StatusCodeResult(StatusCodes.Status412PreconditionFailed);
            }
            else
            {
                
            }

            return NoContent();
        }
    }
}
