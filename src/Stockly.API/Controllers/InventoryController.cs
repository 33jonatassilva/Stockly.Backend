using MediatR;
using Microsoft.AspNetCore.Mvc;
using Stockly.Application.Inventories.Commands;
using Stockly.Application.Inventories.Queries;

namespace Stockly.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly ISender _sender;

        public InventoryController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] bool belowMinimum, CancellationToken cancellationToken)
        {
            var inventories = await _sender.Send(new GetAllInventoriesQuery(belowMinimum), cancellationToken);
            return Ok(inventories);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var inventory = await _sender.Send(new GetInventoryByIdQuery(id), cancellationToken);
            return Ok(inventory);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateInventoryCommand command, CancellationToken cancellationToken)
        {
            var inventory = await _sender.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = inventory.Id }, inventory);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateLevels(Guid id, UpdateInventoryLevelsCommand command, CancellationToken cancellationToken)
        {
            if (id != command.Id)
            {
                return BadRequest("O id da rota difere do id do corpo da requisição.");
            }

            var inventory = await _sender.Send(command, cancellationToken);
            return Ok(inventory);
        }
    }
}
