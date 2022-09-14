using System.ComponentModel;

namespace WebAppWithReflectionAndEventDelegate
{
    public record EventAndHandlerObject(Type EventType, Type HandlerType);

    public class AddTgpEventsAndHandlers
    {
        protected readonly Dictionary<string, EventAndHandlerObject> eventsAndHandlers;

        public AddTgpEventsAndHandlers()
        {
            eventsAndHandlers = new();
        }

        public AddTgpEventsAndHandlers Append<TEventArgs, THandler>()
            where TEventArgs : EventArgs
            where THandler : ITgpEventHandler<TEventArgs>
        {
            string key = typeof(THandler).FullName ?? throw new InvalidCastException("Handler class is invalid.");
            eventsAndHandlers.Add(key, new EventAndHandlerObject(typeof(TEventArgs), typeof(THandler)));
            return this;
        }
    }

    public static class DiServices
    {
        private sealed class GetHandlers : AddTgpEventsAndHandlers
        {
            public List<EventAndHandlerObject> GetAll() 
                => eventsAndHandlers.Select(pair => pair.Value)?.ToList() ?? new();

            public bool NoSubscriber => !eventsAndHandlers.Any();
        }

        private static GetHandlers? HandlersCollection {get; set;}

        public static void AddTgpEvents(this IServiceCollection services, Action<AddTgpEventsAndHandlers> handlers)
        {
            services.AddSingleton<TgpEvents>();

            HandlersCollection = new GetHandlers();
            handlers.Invoke(HandlersCollection);

            if (HandlersCollection.NoSubscriber)
                throw new InvalidOperationException("You need register Handlers here.");

            foreach(var handler in HandlersCollection.GetAll())
                services.AddTransient(handler.HandlerType);
        }

        public static void UseTgp(this IServiceProvider serviceProvider)
        {
            if (HandlersCollection is null || HandlersCollection.NoSubscriber)
                throw new InvalidOperationException($"Você precisa cadastrar seus assinantes utilizando {nameof(AddTgpEvents)}");

            var tgp = serviceProvider.GetRequiredService<TgpEvents>();

            foreach (var pairObject in HandlersCollection.GetAll())
            {
                var handlerMethodInfo = pairObject.HandlerType.GetMethod("Handle", new[] { typeof(object), pairObject.EventType });

                if (handlerMethodInfo is null)
                    continue;

                var eventDelegate = typeof(TgpEvents.AsyncEventHandler<>).MakeGenericType(new[] { pairObject.EventType });

                var delegateSubscriber = handlerMethodInfo.CreateDelegate(eventDelegate, serviceProvider.GetRequiredService(pairObject.HandlerType));

                tgp.Subscribe(pairObject.EventType.Name, delegateSubscriber);
            }
        }
    }

    //public static class DiServicesV2
    //{

    //    public static void UseTgpEvents(this IServiceProvider serviceProvider, IServiceCollection services)
    //    {
    //        IEnumerable<Type> allEventHandlers = services
    //            .Where(x => x.ServiceType.GetInterfaces().Any(iface => iface.Name.Equals(nameof(ITgpEventHandler))))
    //            .Where(x => x.ServiceType != null)
    //            .Select(x => x.ServiceType);

    //        var allEvents = allEventHandlers
    //            .Select(type => new
    //            {
    //                EventHandlerType = type,
    //                EventArgsName = type
    //                    .GetInterfaces()
    //                    .Select(interfaceType => interfaceType.GenericTypeArguments.FirstOrDefault(args => args.BaseType != null && args.BaseType.Name.Equals(nameof(EventArgs))))
    //                    .Select(genericType => genericType?.FullName ?? "NotIdentifier")
    //                    .First()
    //            });

    //        var tgp = serviceProvider.GetRequiredService<TgpEvents>();

    //        foreach (var onePair in allEvents)
    //        {
    //            var eventArgsType = Type.GetType(onePair.EventArgsName);
    //            if (eventArgsType is null)
    //                continue;

    //            var methodInfo = serviceProvider
    //                .GetRequiredService(onePair.EventHandlerType)
    //                .GetType()
    //                .GetMethod("Handle");
    //                ///?.MakeGenericMethod(typeof(object), eventArgsType);

    //            if (methodInfo is null)
    //                continue;

    //            tgp.Subscribe(onePair.EventArgsName, methodInfo);
    //        }
    //    }
    //}

    public interface ITgpEventHandler<in T> where T : EventArgs
    {
        Task Handle(object? sender, T eventArgs);
    }

    public interface ITgpEvents { }
    public class TgpEvents : ITgpEvents
    {
        /// <summary>
        /// Para que eu possa 'cadastrar' os métodos manipuladores (Handler) de forma assíncrona (Task).
        /// </summary>
        /// <typeparam name="T"><see cref="EventArgs"/></typeparam>
        /// <param name="sender">Publisher Object</param>
        /// <param name="eventArgs">Event Content</param>
        /// <returns></returns>
        public delegate Task AsyncEventHandler<in T>(object? sender, T eventArgs) where T : EventArgs;

        /// <summary>
        /// Equivalente a um dicionário de Eventos
        /// </summary>
        private readonly EventHandlerList eventList;
        private readonly IServiceProvider serviceProvider;

        /// <summary>
        /// Para registrar no dicionário de eventos as chaves de acesso aos delegates.
        /// Isso é somente um facilitador, utilizando o nome da classe de EventsArgs como chave.
        /// </summary>
        /// <typeparam name="T"><see cref="EventArgs"/></typeparam>
        /// <returns></returns>
        private static string Key<T>() where T : EventArgs => typeof(T).Name;

        public TgpEvents(IServiceProvider serviceProvider)
        {
            eventList = new EventHandlerList();
            this.serviceProvider = serviceProvider;
        }

        public void Publish<T>(T args, CancellationToken cancellationToken = default) where T : EventArgs
        {
            var @events = eventList[Key<T>()];

            if (@events is null)
            {
                Console.WriteLine("Evento '{0}' publicado, porém não existe nenhum assinante.", Key<T>());
                return;
            }

            ///Para que todos os assitantes possam executar seus blocos de execução utilizando injeção de dependência.
            ///Se eu não aguardar a conclusão dos blocos, pode ocorrer 'dispose' de um objeto sendo utilizado pelo assinante, e a execução falhar por conta disso.
            Task[] eventTasks = @events
                .GetInvocationList()
                .OfType<AsyncEventHandler<T>>()
                .Select(invocationDelegate =>
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    if (args is CancelEventArgs clArgs && clArgs.Cancel)
                    {
                        Console.WriteLine("Evento '{0}' foi cancelado pelo publicador através do CancelEventArgs.", Key<T>());
                        return Task.CompletedTask;
                    }

                    return invocationDelegate(this, args);
                })
                .ToArray();

            Task.WaitAll(eventTasks, cancellationToken);
        }

        public void Subscribe<T>(AsyncEventHandler<T> asyncDelegate) where T : EventArgs
        {
            eventList.RemoveHandler(Key<T>(), asyncDelegate);
            eventList.AddHandler(Key<T>(), asyncDelegate);
        }

        public void Subscribe<TEvent, TEventHandler>()
            where TEvent : EventArgs
            where TEventHandler : ITgpEventHandler<TEvent>
        {
            Subscribe<TEvent>(serviceProvider.GetRequiredService<TEventHandler>().Handle);
        }

        public void Subscribe(string key, Delegate @delegate)
        {
            eventList.RemoveHandler(key, @delegate);
            eventList.AddHandler(key, @delegate);
        }
    }
}