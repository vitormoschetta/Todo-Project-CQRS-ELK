namespace Todo.Domain.Events.Handlers
{
    public class LoggingEventHandler :
                        IEventHandler<CreatedTodoItemNotification>,
                        IEventHandler<UpdatedTodoItemNotification>,
                        IEventHandler<DeletedTodoItemNotification>,
                        IEventHandler<MarkedAsDoneTodoItemNotification>
    {
        private readonly ILogger<LoggingEventHandler> _logger;

        public LoggingEventHandler(ILogger<LoggingEventHandler> logger)
        {
            _logger = logger;
        }

        public async Task Handle(CreatedTodoItemNotification @event)
        {
            await SerializeAndLog(@event, EEventType.Created);
        }

        public async Task Handle(UpdatedTodoItemNotification @event)
        {
            await SerializeAndLog(@event, EEventType.Updated);
        }

        public async Task Handle(DeletedTodoItemNotification @event)
        {
            await SerializeAndLog(@event, EEventType.Deleted);
        }

        public async Task Handle(MarkedAsDoneTodoItemNotification @event)
        {
            await SerializeAndLog(@event, EEventType.Updated);
        }

        private Task SerializeAndLog(object data, EEventType type)
        {
            var message = JsonManagerSerialize.Serialize(
                            new
                            {
                                Type = type.ToString(),
                                Data = data
                            });

            _logger.LogInformation($"LoggingEventHandler: {message}");

            return Task.CompletedTask;
        }
    }
}