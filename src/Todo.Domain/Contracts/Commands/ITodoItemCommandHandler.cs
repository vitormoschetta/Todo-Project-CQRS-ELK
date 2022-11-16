namespace Todo.Domain.Contracts.Commands
{
    public interface ITodoItemCommandHandler
    {
        Task<CommandResponse> Handle(CreateTodoItemRequest request);
        Task<CommandResponse> Handle(UpdateTodoItemRequest request);
        Task<CommandResponse> Handle(DeleteTodoItemRequest request);
        Task<CommandResponse> Handle(MarkAsDoneTodoItemRequest request);
    }
}