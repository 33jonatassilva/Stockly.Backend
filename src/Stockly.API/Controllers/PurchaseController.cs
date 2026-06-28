using MediatR;
using Microsoft.AspNetCore.Mvc;
using Stockly.Application.Purchases.Commands;
using Stockly.Application.Purchases.Queries;

namespace Stockly.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly ISender _sender;

        public PurchaseController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] DateTime? start, [FromQuery] DateTime? end, CancellationToken cancellationToken)
        {
            var purchases = await _sender.Send(new GetAllPurchasesQuery(start, end), cancellationToken);
            return Ok(purchases);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var purchase = await _sender.Send(new GetPurchaseByIdQuery(id), cancellationToken);
            return Ok(purchase);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePurchaseCommand command, CancellationToken cancellationToken)
        {
            var purchase = await _sender.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = purchase.Id }, purchase);
        }
    }
}
