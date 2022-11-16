namespace Todo.Domain.Commands.Handlers
{
    public class TodoItemCommandHandler :
                        IRequestHandler<CreateTodoItemRequest, CommandResponse>,
                        IRequestHandler<DeleteTodoItemRequest, CommandResponse>,
                        IRequestHandler<MarkAsDoneTodoItemRequest, CommandResponse>,
                        IRequestHandler<UpdateTodoItemRequest, CommandResponse>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMediator _mediator;
        private readonly ILogger<TodoItemCommandHandler> _logger;

        public TodoItemCommandHandler(IUnitOfWork uow, IMediator mediator, ILogger<TodoItemCommandHandler> logger)
        {
            _uow = uow;
            _mediator = mediator;
            _logger = logger;
        }

        public async Task<CommandResponse> Handle(CreateTodoItemRequest request, CancellationToken cancellationToken)
        {
            if (request.IsInvalid)
                return CommandResponse.Fail(request.Errors);

            if (await _uow.TodoItems.Exists(request.Title))
                return CommandResponse.Fail("Item já existe");

            var todoItem = request.ToEntity();

            // TODO: Abrir transação para salvar para confirmar a transação apena depois de enviar a mensagem. 
            // Se houver erro ao enviar a mensagem, fazer rollback.

            await _uow.TodoItems.Add(todoItem);
            await _uow.Commit();

            await _mediator.Publish(new CreatedTodoItemNotification(todoItem), cancellationToken);

            return CommandResponse.Ok;
        }

        public async Task<CommandResponse> Handle(UpdateTodoItemRequest request, CancellationToken cancellationToken)
        {
            if (request.IsInvalid)
                return CommandResponse.Fail(request.Errors);

            var todoItem = await _uow.TodoItems.GetById(request.Id);

            if (todoItem == null)
                return CommandResponse.Fail("Item não encontrado");

            todoItem.Update(request.Title, request.Done);

            await _uow.Commit();

            await _mediator.Publish(new UpdatedTodoItemNotification(todoItem), cancellationToken);

            return CommandResponse.Ok;
        }

        public async Task<CommandResponse> Handle(DeleteTodoItemRequest request, CancellationToken cancellationToken)
        {
            if (request.IsInvalid)
                return CommandResponse.Fail(request.Errors);

            var todoItem = await _uow.TodoItems.GetById(request.Id);

            if (todoItem == null)
                return CommandResponse.Fail("Item não encontrado");

            await _uow.TodoItems.Remove(todoItem);
            await _uow.Commit();

            await _mediator.Publish(new DeletedTodoItemNotification(todoItem), cancellationToken);

            return CommandResponse.Ok;
        }

        public async Task<CommandResponse> Handle(MarkAsDoneTodoItemRequest request, CancellationToken cancellationToken)
        {
            if (request.IsInvalid)
                return CommandResponse.Fail(request.Errors);

            var todoItem = await _uow.TodoItems.GetById(request.Id);

            if (todoItem == null)
                return CommandResponse.Fail("Item não encontrado");

            todoItem.MarkAsDone();

            await _uow.Commit();

            await _mediator.Publish(new MarkedAsDoneTodoItemNotification(todoItem), cancellationToken);

            return CommandResponse.Ok;
        }
    }
}