using MediatR;
using SharedDomain.Repositories;
using WebAppWithMediatR.Notifications;

namespace WebAppWithMediatR.CQRS.Commands
{
    public class CreateOccurrencyCommandHandler : IRequestHandler<CreateOccurrencyCommand>
    {
        private readonly OccurrencyRepository occurrencyRepository;
        private readonly IMediator mediator;

        public CreateOccurrencyCommandHandler(OccurrencyRepository occurrencyRepository, IMediator mediator)
        {
            this.occurrencyRepository = occurrencyRepository;
            this.mediator = mediator;
        }

        public async Task<Unit> Handle(CreateOccurrencyCommand request, CancellationToken cancellationToken)
        {
            var result = await occurrencyRepository.Insert(request.Model);

            if (result is null)
                throw new AggregateException($"blablabla, deu erro ao tentar persistir uma nova ocorrência no banco de dados.");

            await mediator.Publish(new OccurrencyCreatedNotification(result), cancellationToken);
            return Unit.Value;
        }
    }
}