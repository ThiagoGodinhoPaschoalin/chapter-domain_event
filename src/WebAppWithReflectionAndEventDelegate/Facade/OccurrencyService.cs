using SharedDomain.Models;
using SharedDomain.Repositories;

namespace WebAppWithReflectionAndEventDelegate.Facade
{
    public class OccurrencyService
    {
        private readonly OccurrencyRepository occurrencyRepository;

        public OccurrencyService(OccurrencyRepository occurrencyRepository)
        {
            this.occurrencyRepository = occurrencyRepository;
        }

        public Task<IEnumerable<OccurrencyModel>> GetAll() => occurrencyRepository.GetAll();

        public Task<OccurrencyModel?> GetOne(Guid id) => occurrencyRepository.GetOne(id);
    }
}