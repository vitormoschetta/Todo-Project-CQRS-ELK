using Microsoft.Extensions.DependencyInjection;

namespace Todo.Domain
{
    public class Mediator : IMediator
    {
        private readonly IServiceProvider _serviceProvider;

        public Mediator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task Publish<T>(T @event) where T : IDomainEvent
        {
            var type = @event.GetType();

            var handlerType = typeof(IEventHandler<>).MakeGenericType(type);

            var handlers = _serviceProvider.GetServices(handlerType);

            foreach (var handler in handlers)
            {                
                var method = handler
                                .GetType()
                                .GetMethods()
                                .Where(m => m.Name == "Handle" && m.GetParameters().Length == 1 && m.GetParameters()[0].ParameterType == type)
                                .FirstOrDefault();
                
                await (Task)method.Invoke(handler, new object[] { @event });
            }
        }
    }
}