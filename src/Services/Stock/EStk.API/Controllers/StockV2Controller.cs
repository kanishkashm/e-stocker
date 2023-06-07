using Estk.Core.Domain;
using Estk.Core.Features.StockItem.Command.AddStockItem;
using Estk.Core.Features.StockItem.Command.UpdateStockItem;
using Estk.Core.Features.StockItem.Query.GetStockItems;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EStk.API.Controllers
{
    [Route("api/v2/Stock")]
    [ApiController]
    public class StockV2Controller : ControllerBase
    {
        private readonly IMediator _mediator;

        public StockV2Controller(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{stockId}")]
        //[Authorize(Roles = ("Admin,User,Auditor"))]
        public async Task<ActionResult> Get(int stockId)
        {
            var query = new GetStockItemsQuery(stockId);
            var stock = await _mediator.Send(query);
            return Ok(stock);
        }

        [HttpPost]
        //[Authorize(Roles = ("Admin"))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async  Task<ActionResult<int>> Post([FromBody] StockCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut]
        //[Authorize(Roles = ("Admin,User"))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Put(int itemId, [FromBody] Item item)
        {
            var updateCommand = new UpdateStockItemCommand
            {
                Id = itemId,
                Name = item.Name,
                Price = item.Price,
            };
            await _mediator.Send(updateCommand);
            return NoContent();
        }



        //[HttpPut("issueStock/{stockId}")]
        //public async Task<IActionResult> IssueStock(int? stockId, byte[] rowVersion)
        //{

        //}
    }
}
