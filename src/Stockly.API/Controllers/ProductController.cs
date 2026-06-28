using MediatR;
using Microsoft.AspNetCore.Mvc;
using Stockly.Application.Products.Commands;
using Stockly.Application.Products.Queries;

namespace Stockly.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ISender _sender;

        public ProductController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] bool onlyActive, CancellationToken cancellationToken)
        {
            var products = await _sender.Send(new GetAllProductsQuery(onlyActive), cancellationToken);
            return Ok(products);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var product = await _sender.Send(new GetProductByIdQuery(id), cancellationToken);
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var product = await _sender.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, UpdateProductCommand command, CancellationToken cancellationToken)
        {
            if (id != command.Id)
            {
                return BadRequest("O id da rota difere do id do corpo da requisição.");
            }

            var product = await _sender.Send(command, cancellationToken);
            return Ok(product);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await _sender.Send(new DeleteProductCommand(id), cancellationToken);
            return NoContent();
        }

        [HttpGet("{id:guid}/variations")]
        public async Task<IActionResult> GetVariations(Guid id, CancellationToken cancellationToken)
        {
            var variations = await _sender.Send(new GetProductVariationsQuery(id), cancellationToken);
            return Ok(variations);
        }

        [HttpPost("{id:guid}/variations")]
        public async Task<IActionResult> CreateVariation(Guid id, CreateProductVariationRequest request, CancellationToken cancellationToken)
        {
            var command = new CreateProductVariationCommand(id, request.PackageDescription, request.QuantityPerPackage, request.Unit);
            var variation = await _sender.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetVariations), new { id }, variation);
        }
    }

    public record CreateProductVariationRequest(string PackageDescription, decimal QuantityPerPackage, string Unit);
}
