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

        public Task Publish<T>(T @event) where T : IDomainEvent
        {
            var type = @event.GetType();

            var handlerType = typeof(IEventHandler<>).MakeGenericType(type);

            var handlers = _serviceProvider.GetServices(handlerType);

            foreach (var handler in handlers)
            {
                if (handler == null)
                    continue;

                var method = handler
                                .GetType()
                                .GetMethods()
                                .Where(m => m.Name == "Handle" && m.GetParameters().Length == 1 && m.GetParameters()[0].ParameterType == type)
                                .FirstOrDefault();

                if (method == null)
                    continue;

                method.Invoke(handler, new object[] { @event });
            }

            return Task.CompletedTask;
        }


        public async Task PublishAsync<T>(T @event) where T : IDomainEvent
        {
            var type = @event.GetType();

            var handlerType = typeof(IEventHandler<>).MakeGenericType(type);

            var handlers = _serviceProvider.GetServices(handlerType);

            foreach (var handler in handlers)
            {
                if (handler == null)
                    continue;

                var method = handler
                                .GetType()
                                .GetMethods()
                                .Where(m => m.Name == "Handle" && m.GetParameters().Length == 1 && m.GetParameters()[0].ParameterType == type)
                                .FirstOrDefault();

                if (method == null)
                    continue;

                method.Invoke(handler, new object[] { @event });
            }

            await Task.CompletedTask;
        }
    }
}