
namespace Todo.Domain.Test.Command.Handler
{
    public class CreateTodoItemTest
    {
        private readonly IUnitOfWork _uow;
        private readonly IMediator _mediator;
        private readonly ILogger<TodoItemCommandHandler> _logger;
        private readonly IMessageService _messageService;
        private readonly TodoItemCommandHandler _handler;

        public CreateTodoItemTest()
        {
            _uow = Substitute.For<IUnitOfWork>();
            _mediator = Substitute.For<IMediator>();
            _logger = new Logger<TodoItemCommandHandler>(new LoggerFactory());
            _messageService = Substitute.For<IMessageService>();
            _handler = new TodoItemCommandHandler(_uow, _mediator, _logger);
        }

        [Fact]
        public async Task Test1()
        {
            // Arrange
            var command = new CreateTodoItemRequest()
            {
                Title = "Todo Xpto",
                Done = false
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Success);
        }

        [Fact]
        public async Task Test2()
        {
            // Arrange
            var command = new CreateTodoItemRequest()
            {
                Title = "To",
                Done = false
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Success);
            Assert.Equal("O título deve conter pelo menos 3 caracteres", result.Errors.First());
        }
    }
}