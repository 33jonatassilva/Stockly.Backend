using MediatR;
using Microsoft.AspNetCore.Mvc;
using Stockly.Application.Categories.Commands;
using Stockly.Application.Categories.Queries;

namespace Stockly.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ISender _sender;

        public CategoryController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var categories = await _sender.Send(new GetAllCategoriesQuery(), cancellationToken);
            return Ok(categories);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var category = await _sender.Send(new GetCategoryByIdQuery(id), cancellationToken);
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryCommand command, CancellationToken cancellationToken)
        {
            var category = await _sender.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = category.Id }, category);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, UpdateCategoryCommand command, CancellationToken cancellationToken)
        {
            if (id != command.Id)
            {
                return BadRequest("O id da rota difere do id do corpo da requisição.");
            }

            var category = await _sender.Send(command, cancellationToken);
            return Ok(category);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await _sender.Send(new DeleteCategoryCommand(id), cancellationToken);
            return NoContent();
        }
    }
}
