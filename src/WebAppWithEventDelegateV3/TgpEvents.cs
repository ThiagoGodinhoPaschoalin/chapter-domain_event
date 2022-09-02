using System.ComponentModel;

namespace WebAppWithEventDelegateV3
{
    public static class DiServices
    {
        public static TgpEvents UseTgpEvents(this IServiceProvider serviceProvider)
        {
            return serviceProvider.GetRequiredService<TgpEvents>();
        }

        public static TgpEvents AppendSubscriber<TEvent, TEventHandler>(this TgpEvents tgpEvents)
            where TEvent : EventArgs
            where TEventHandler : ITgpEventHandler<TEvent>
        {
            tgpEvents.Subscribe<TEvent, TEventHandler>();
            return tgpEvents;
        }
    }

    public interface ITgpEventHandler<in TEvent> 
        where TEvent : EventArgs
    {
        Task Handle(object sender, TEvent eventArgs);
    }

    public class TgpEvents
    {
        /// <summary>
        /// Para que eu possa 'cadastrar' os métodos manipuladores (Handler) de forma assíncrona (Task).
        /// </summary>
        /// <typeparam name="T"><see cref="EventArgs"/></typeparam>
        /// <param name="sender">Publisher Object</param>
        /// <param name="eventArgs">Event Content</param>
        /// <returns></returns>
        public delegate Task AsyncEventHandler<in T>(object sender, T eventArgs) where T : EventArgs;

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

        public void Unsubscribe<T>(AsyncEventHandler<T> asyncDelegate) where T : EventArgs
        {
            eventList.RemoveHandler(Key<T>(), asyncDelegate);
        }
    }
}