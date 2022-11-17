namespace Todo.Domain.Contracts.Events
{
    public interface IEventHandler<T> where T : IDomainEvent
    {

    }
}