using MediatR;

namespace WebAppWithMediatR.Notifications
{
    public class OccurrencyCreatedNotificationHandler : INotificationHandler<OccurrencyCreatedNotification>
    {
        private readonly ILogger<OccurrencyCreatedNotificationHandler> logger;

        public OccurrencyCreatedNotificationHandler(ILogger<OccurrencyCreatedNotificationHandler> logger)
        {
            this.logger = logger;
        }

        public Task Handle(OccurrencyCreatedNotification notification, CancellationToken cancellationToken)
        {
            logger.LogInformation("Uma nova ocorrência foi gerada e quem se interessar por isso é só criar o seu NoticationHandler! ;) ");
            logger.LogInformation("Ocorrência que foi criada é a {0}.", notification.Model);
            return Task.CompletedTask;
        }
    }
}