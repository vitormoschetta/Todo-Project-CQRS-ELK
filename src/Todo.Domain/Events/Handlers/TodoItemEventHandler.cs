namespace Todo.Domain.Events.Handlers
{
    public class TodoItemEventHandler : ITodoItemEventHandler
    {
        private readonly IMessageService _messageService;

        public TodoItemEventHandler(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public async Task Handle(CreatedTodoItemNotification notification)
        {
            await SendNotification(notification, EMessageType.Created);
        }

        public async Task Handle(DeletedTodoItemNotification notification)
        {
            await SendNotification(notification, EMessageType.Deleted);
        }

        public async Task Handle(MarkedAsDoneTodoItemNotification notification)
        {
            await SendNotification(notification, EMessageType.Updated);
        }

        public async Task Handle(UpdatedTodoItemNotification notification)
        {
            await SendNotification(notification, EMessageType.Updated);
        }

        private Task SendNotification(object data, EMessageType type)
        {
            var message = JsonManagerSerialize.Serialize(
                            new
                            {
                                Type = type.ToString(),
                                Data = data
                            });

            _messageService.Send(message);

            return Task.CompletedTask;
        }
    }
}