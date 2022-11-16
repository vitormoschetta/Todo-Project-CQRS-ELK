namespace Todo.Domain.Contracts.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        ITodoItemRepository TodoItems { get; }
        Task Commit();
        void ClearContext();
    }
}