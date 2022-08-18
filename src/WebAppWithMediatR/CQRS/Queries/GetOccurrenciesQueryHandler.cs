using MediatR;
using SharedDomain.Models;
using SharedDomain.Repositories;

namespace WebAppWithMediatR.CQRS.Queries
{
    public class GetOccurrenciesQueryHandler : IRequestHandler<GetOccurrenciesQuery, IEnumerable<OccurrencyModel>>
    {
        private readonly OccurrencyRepository occurrencyRepository;

        public GetOccurrenciesQueryHandler(OccurrencyRepository occurrencyRepository)
        {
            this.occurrencyRepository = occurrencyRepository;
        }

        public Task<IEnumerable<OccurrencyModel>> Handle(GetOccurrenciesQuery request, CancellationToken cancellationToken)
        {
            return occurrencyRepository.GetAll();
        }
    }
}