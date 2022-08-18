using MediatR;
using SharedDomain.Models;
using SharedDomain.Repositories;

namespace WebAppWithMediatR.CQRS.Queries
{
    public class GetOccurrencyQueryHandler : IRequestHandler<GetOccurrencyQuery, OccurrencyModel?>
    {
        private readonly OccurrencyRepository occurrencyRepository;

        public GetOccurrencyQueryHandler(OccurrencyRepository occurrencyRepository)
        {
            this.occurrencyRepository = occurrencyRepository;
        }

        public Task<OccurrencyModel?> Handle(GetOccurrencyQuery request, CancellationToken cancellationToken)
        {
            return occurrencyRepository.GetOne(request.Id);
        }
    }
}