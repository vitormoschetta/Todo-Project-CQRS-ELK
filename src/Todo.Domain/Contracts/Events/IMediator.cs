namespace Todo.Domain.Contracts.Events
{
    public interface IMediator
    {
        Task Publish<T>(T @event) where T : IDomainEvent;
    }
}