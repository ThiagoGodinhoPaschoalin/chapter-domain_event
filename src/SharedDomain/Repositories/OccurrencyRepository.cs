using Microsoft.Extensions.Logging;
using SharedDomain.Contexts;
using SharedDomain.Models;

namespace SharedDomain.Repositories
{
    public class OccurrencyRepository
    {
        private readonly ApplicationDbContext context;
        private readonly ILogger<OccurrencyRepository> logger;

        public OccurrencyRepository(ApplicationDbContext context, ILogger<OccurrencyRepository> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public Task<OccurrencyModel> Insert(OccurrencyModel model)
        {
            context.Add(model);
            context.SaveChanges();
            logger.LogInformation("OccurrencyRepository Executado. Data: {0}.", model);
            return Task.FromResult(model);
        }

        public Task<IEnumerable<OccurrencyModel>> GetAll()
            => Task.FromResult(context.Occurrencies.ToList().AsEnumerable());

        public Task<OccurrencyModel?> GetOne(Guid id)
            => Task.FromResult(context.Occurrencies.Find(id));
    }
}