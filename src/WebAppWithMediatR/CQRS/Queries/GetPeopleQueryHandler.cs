using MediatR;
using SharedDomain.Models;
using SharedDomain.Repositories;

namespace WebAppWithMediatR.CQRS.Queries
{
    public class GetPeopleQueryHandler : IRequestHandler<GetPeopleQuery, IEnumerable<PersonModel>>
    {
        private readonly PersonRepository personRepository;

        public GetPeopleQueryHandler(PersonRepository personRepository)
        {
            this.personRepository = personRepository;
        }

        public Task<IEnumerable<PersonModel>> Handle(GetPeopleQuery request, CancellationToken cancellationToken)
        {
            return personRepository.GetAll();
        }
    }
}
