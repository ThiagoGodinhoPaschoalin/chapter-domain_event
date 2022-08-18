using MediatR;
using SharedDomain.Models;
using SharedDomain.Repositories;
using WebAppWithMediatR.Notifications;

namespace WebAppWithMediatR.CQRS.Commands
{
    public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, Guid>
    {
        private readonly PersonRepository personRepository;
        private readonly IMediator mediator;

        public CreatePersonCommandHandler(PersonRepository personRepository, IMediator mediator)
        {
            this.personRepository = personRepository;
            this.mediator = mediator;
        }

        public async Task<Guid> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            var result = await personRepository.Insert(new PersonModel(request.Name));

            if (result is null)
                throw new AggregateException($"blabla, deu erro ao tentar persistir pessoa.");

            await mediator.Publish(new PersonCreatedNotification(result));

            return result.Id;
        }
    }
}