using Microsoft.AspNetCore.Mvc;
using TodoList.Api.Application.CommandHandlers;
using TodoList.Api.Application.Commands;
using TodoList.Api.Application.QueryHandlers;
using TodoList.Api.Application.Queries;

namespace TodoList.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly CreateTodoCommandHandler _createHandler;
        private readonly UpdateTodoCommandHandler _updateHandler;
        private readonly DeleteTodoCommandHandler _deleteHandler;
        private readonly GetAllTodoQueryHandler _getAllHandler;
        private readonly GetTodoByIdQueryHandler _getByIdHandler;

        public TodoController(
            CreateTodoCommandHandler createHandler,
            UpdateTodoCommandHandler updateHandler,
            DeleteTodoCommandHandler deleteHandler,
            GetAllTodoQueryHandler getAllHandler,
            GetTodoByIdQueryHandler getByIdHandler)
        {
            _createHandler = createHandler;
            _updateHandler = updateHandler;
            _deleteHandler = deleteHandler;
            _getAllHandler = getAllHandler;
            _getByIdHandler = getByIdHandler;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var todos = await _getAllHandler.HandleAsync(new GetAllTodoQuery());
            return Ok(todos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var todo = await _getByIdHandler.HandleAsync(new GetTodoByIdQuery { Id = id });

            if (todo == null)
                return NotFound();

            return Ok(todo);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTodoCommand command)
        {
            var todo = await _createHandler.HandleAsync(command);
            return CreatedAtAction(nameof(GetById), new { id = todo.Id }, todo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            Guid id,
            [FromBody] UpdateTodoCommand command)
        {
            var isUpdated = await _updateHandler.HandleAsync(id, command);

            if (!isUpdated)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var isDeleted = await _deleteHandler.HandleAsync(new DeleteTodoCommand { Id = id });

            if (!isDeleted)
                return NotFound();

            return NoContent();
        }
    }
}
