using MediatR;
using Microsoft.AspNetCore.Mvc;
using Stockly.Application.Brands.Commands;
using Stockly.Application.Brands.Queries;

namespace Stockly.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly ISender _sender;

        public BrandController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var brands = await _sender.Send(new GetAllBrandsQuery(), cancellationToken);
            return Ok(brands);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var brand = await _sender.Send(new GetBrandByIdQuery(id), cancellationToken);
            return Ok(brand);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBrandCommand command, CancellationToken cancellationToken)
        {
            var brand = await _sender.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = brand.Id }, brand);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, UpdateBrandCommand command, CancellationToken cancellationToken)
        {
            if (id != command.Id)
            {
                return BadRequest("O id da rota difere do id do corpo da requisição.");
            }

            var brand = await _sender.Send(command, cancellationToken);
            return Ok(brand);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await _sender.Send(new DeleteBrandCommand(id), cancellationToken);
            return NoContent();
        }
    }
}
