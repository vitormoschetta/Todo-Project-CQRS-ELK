namespace Todo.Domain.Events.Handlers
{
    public class TodoItemEventHandler :
                        IEventHandler<CreatedTodoItemNotification>,
                        IEventHandler<UpdatedTodoItemNotification>,
                        IEventHandler<DeletedTodoItemNotification>,
                        IEventHandler<MarkedAsDoneTodoItemNotification>

    {
        private readonly IMessageService _messageService;

        public TodoItemEventHandler(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public async Task Handle(CreatedTodoItemNotification @event)
        {
            await SendNotification(@event, EEventType.Created);
        }

        public async Task Handle(UpdatedTodoItemNotification @event)
        {
            await SendNotification(@event, EEventType.Updated);
        }

        public async Task Handle(DeletedTodoItemNotification @event)
        {
            await SendNotification(@event, EEventType.Deleted);
        }

        public async Task Handle(MarkedAsDoneTodoItemNotification @event)
        {
            await SendNotification(@event, EEventType.Updated);
        }

        private Task SendNotification(object data, EEventType type)
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