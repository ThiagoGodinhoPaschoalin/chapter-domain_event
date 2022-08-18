using MediatR;
using SharedDomain.Models;
using WebAppWithMediatR.CQRS.Commands;

namespace WebAppWithMediatR.Notifications
{
    public class PersonCreatedNotificationHandler : INotificationHandler<PersonCreatedNotification>
    {
        private readonly ILogger<PersonCreatedNotificationHandler> logger;

        public PersonCreatedNotificationHandler(ILogger<PersonCreatedNotificationHandler> logger)
        {
            this.logger = logger;
        }

        public Task Handle(PersonCreatedNotification notification, CancellationToken cancellationToken)
        {
            logger.LogInformation("OBA! Usuário '{0}' foi criado com sucesso!", notification.Model);
            return Task.CompletedTask;
        }
    }

    public class CreateOccurrencyWhenPersonCreatedNotificationHandler : INotificationHandler<PersonCreatedNotification>
    {
        private readonly IMediator mediator;

        public CreateOccurrencyWhenPersonCreatedNotificationHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public Task Handle(PersonCreatedNotification notification, CancellationToken cancellationToken)
        {
            var occurrency = new OccurrencyModel(OccurrencyType.CreatePerson, notification.Model);
            var command = new CreateOccurrencyCommand(occurrency);
            mediator.Send(command, cancellationToken);
            return Task.CompletedTask;
        }
    }

    public class RegisterLogWhenPersonCreatedNotificationHandler : INotificationHandler<PersonCreatedNotification>
    {
        private readonly ILogger<RegisterLogWhenPersonCreatedNotificationHandler> logger;

        public RegisterLogWhenPersonCreatedNotificationHandler(ILogger<RegisterLogWhenPersonCreatedNotificationHandler> logger)
        {
            this.logger = logger;
        }
        public Task Handle(PersonCreatedNotification notification, CancellationToken cancellationToken)
        {
            logger.LogInformation("Simulação: Foi enviado um comando para criar um log para o usuário '{0}' criado!", notification.Model);
            return Task.CompletedTask;
        }
    }
}