using MediatR;
using SharedDomain.Models;
using SharedDomain.Repositories;

namespace WebAppWithMediatR.CQRS.Queries
{
    public class GetPersonQueryHandler : IRequestHandler<GetPersonQuery, PersonModel?>
    {
        private readonly PersonRepository personRepository;

        public GetPersonQueryHandler(PersonRepository personRepository)
        {
            this.personRepository = personRepository;
        }

        public Task<PersonModel?> Handle(GetPersonQuery request, CancellationToken cancellationToken)
        {
            return personRepository.GetOne(request.Id);
        }
    }
}
