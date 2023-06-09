﻿using Estk.Core.Domain;
using Estk.Core.Features.StockItem.Command.AddStockItem;
using Estk.Core.Features.StockItem.Command.IssueStock;
using Estk.Core.Features.StockItem.Command.UpdateStockItem;
using Estk.Core.Features.StockItem.Query.GetStockItems;
using EStk.API.Data;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EStk.API.Controllers
{
    [Route("api/v2/Stock")]
    [ApiController]
    [Authorize]
    public class StockV2Controller : ControllerBase
    {
        private readonly IMediator _mediator;

        public StockV2Controller(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{stockId}")]
        [Authorize(Roles = ("Admin,User,Auditor"))]
        public async Task<ActionResult> Get(int stockId)
        {
            var query = new GetStockItemsQuery(stockId);
            var stock = await _mediator.Send(query);
            return Ok(stock);
        }

        [HttpPost]
        [Authorize(Roles = ("Admin"))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> Post([FromBody] StockCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("{itemId}", Name = "UpdateStockItem")]
        [Authorize(Roles = ("Admin,User"))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateStockItem(int itemId, [FromBody] UpdateStockItemCommand item)
        {
            item.Id = itemId;
            await _mediator.Send(item);
            return NoContent();
        }



        [HttpPut("issueStock/{stockId}")]
        [AllowAnonymous]
        public async Task<IActionResult> IssueStock(int? stockId, [FromBody] IssueStockCommand issueStock)
        {
            issueStock.StockId = stockId.Value;
            await _mediator.Send(issueStock);
            return NoContent();
        }
    }
}
