using Microsoft.EntityFrameworkCore;
using Todo.Domain.Contracts.Repositories;

namespace Todo.Infrastructure.Database.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public ITodoItemRepository TodoItems => new TodoItemRepository(_context);

        public void ClearContext()
        {
            _context.ChangeTracker.Entries().ToList().ForEach(x => x.State = EntityState.Detached);
            // _context.ChangeTracker.Clear();
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}