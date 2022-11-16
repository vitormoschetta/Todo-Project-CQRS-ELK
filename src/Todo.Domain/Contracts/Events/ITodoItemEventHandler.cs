namespace Todo.Domain.Contracts.Events
{
    public interface ITodoItemEventHandler
    {
        Task Handle(CreatedTodoItemNotification notification);
        Task Handle(DeletedTodoItemNotification notification);
        Task Handle(MarkedAsDoneTodoItemNotification notification);
        Task Handle(UpdatedTodoItemNotification notification);
    }
}