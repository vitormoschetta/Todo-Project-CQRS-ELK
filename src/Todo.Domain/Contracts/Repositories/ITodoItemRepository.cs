namespace Todo.Domain.Contracts.Repositories
{
    public interface ITodoItemRepository
    {
        Task Add(TodoItem item);
        Task Update(TodoItem item);
        Task Remove(TodoItem item);
        Task UpdateAllToDone();
        Task<IEnumerable<TodoItem>> GetAll();
        Task<TodoItem> GetById(Guid id);
        Task<bool> Exists(string title);
        IQueryable<TodoItem> Query(Func<IQueryable<TodoItem>, IIncludableQueryable<TodoItem, object>> include = null!);
        IQueryable<TodoItem> QueryAsNoTracking(Func<IQueryable<TodoItem>, IIncludableQueryable<TodoItem, object>> include = null!);
    }
}