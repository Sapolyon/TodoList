using Microsoft.AspNetCore.Mvc;
using Todolist2.Application.CommandHandlers;
using Todolist2.Application.Commands;
using Todolist2.Application.QuarieHandlers;
using Todolist2.Application.Quaries;

namespace Todolist2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly CTodoCommandHandlers _createHandler;
        private readonly UTodoCommandHandlers _updateHandler;
        private readonly DTodoCommandHandlers _deleteHandler;
        private readonly GetAllTodoQueryHandler _getAllHandler;
        private readonly GetTodoByIdquarieHandlers _getByIdHandler;

        public TodoController(
            CTodoCommandHandlers createHandler,
            UTodoCommandHandlers updateHandler,
            DTodoCommandHandlers deleteHandler,
            GetAllTodoQueryHandler getAllHandler,
            GetTodoByIdquarieHandlers getByIdHandler)
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
            var todos = await _getAllHandler.Handle();
            return Ok(todos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var todo = await _getByIdHandler.Handle(id);

            if (todo == null)
                return NotFound();

            return Ok(todo);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CTodocommands command)
        {
            await _createHandler.Handle(command);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UTodocommands command)
        {
            command.Id = id;

            var isUpdated = await _updateHandler.Handle(command);

            if (!isUpdated)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _deleteHandler.HandleAsync(new DTodocommands { Id = id });
            return Ok();
        }
    }
}
