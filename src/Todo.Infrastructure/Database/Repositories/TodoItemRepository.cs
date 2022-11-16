using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Todo.Domain.Contracts.Repositories;
using Todo.Domain.Entities;
using Todo.Domain.Exceptions;

namespace Todo.Infrastructure.Database.Repositories
{
    public class TodoItemRepository : ITodoItemRepository
    {
        protected readonly AppDbContext _context;

        public TodoItemRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Add(TodoItem item)
        {
            await _context.TodoItems.AddAsync(item);
        }

        public async Task Update(TodoItem item)
        {
            _context.TodoItems.Update(item);
            await Task.CompletedTask;
        }

        public async Task Remove(TodoItem item)
        {
            _context.TodoItems.Remove(item);
            await Task.CompletedTask;
        }

        public async Task UpdateAllToDone()
        {
            await _context.TodoItems.ForEachAsync(x => x.Done = true);
        }

        public async Task<IEnumerable<TodoItem>> GetAll()
        {
            return await _context.TodoItems.AsNoTracking().ToListAsync();
        }

        public async Task<TodoItem> GetById(Guid id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
                throw new NotFoundException("Todo item not found");

            return todoItem;
        }

        public async Task<bool> Exists(string title)
        {
            return await _context.TodoItems.FirstOrDefaultAsync(x => x.Title == title) != null;
        }

        public IQueryable<TodoItem> Query(Func<IQueryable<TodoItem>, IIncludableQueryable<TodoItem, object>> include = null!)
        {
            IQueryable<TodoItem> query = _context.Set<TodoItem>();

            if (include != null)
            {
                query = include(query);
            }

            return query;
        }

        public IQueryable<TodoItem> QueryAsNoTracking(Func<IQueryable<TodoItem>, IIncludableQueryable<TodoItem, object>> include = null!)
        {
            IQueryable<TodoItem> query = _context.Set<TodoItem>().AsNoTracking();

            if (include != null)
            {
                query = include(query);

            }

            return query;
        }
    }
}