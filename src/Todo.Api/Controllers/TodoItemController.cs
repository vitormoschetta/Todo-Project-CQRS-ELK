using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todo.Application.Commands.Requests;
using Todo.Domain.Commands;
using Todo.Domain.Entities;
using Todo.Infrastructure.Database;

namespace Todo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoItemController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly AppDbContext _context;

        public TodoItemController(IMediator mediator, AppDbContext context)
        {
            _mediator = mediator;
            _context = context;
        }


        [HttpPost]
        public async Task<ActionResult<CommandResponse>> Create([FromBody] CreateTodoItemRequest request)
        {
            var response = await _mediator.Send(request);            

            if (response.Success == false)
                return BadRequest(response);

            return Ok(response);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<CommandResponse>> Update(Guid id, [FromBody] UpdateTodoItemRequest request)
        {
            if (id != request.Id)
                return BadRequest(CommandResponse.Fail("Id da requisição não confere com o id do recurso"));

            var response = await _mediator.Send(request);

            if (response.Success == false)
                return BadRequest(response);

            return Ok(response);
        }


        [HttpDelete()]
        public async Task<ActionResult<CommandResponse>> Delete([FromQuery] DeleteTodoItemRequest request)
        {
            var response = await _mediator.Send(request);

            if (response.Success == false)
                return BadRequest(response);

            return Ok(response);
        }


        [HttpGet()]
        public async Task<IEnumerable<TodoItem>> GetAll()
        {
            return await _context.TodoItems.ToListAsync();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetById(Guid id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
                return NotFound();

            return Ok(todoItem);
        }

        [HttpGet("GetByTitle/{title}")]
        public async Task<ActionResult<TodoItem>> GetByTitle(string title)
        {
            var todoItem = await _context.TodoItems.FirstOrDefaultAsync(x => x.Title == title);

            if (todoItem == null)
                return NotFound();

            return Ok(todoItem);
        }
    }
}