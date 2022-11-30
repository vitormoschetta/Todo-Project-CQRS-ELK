namespace Todo.Domain.Contracts.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        ITodoItemRepository TodoItems { get; }
        Task Commit();
        Task BeginTransaction();
        Task CommitTransaction();
        Task RollbackTransaction();
        void ClearContext();
    }
}