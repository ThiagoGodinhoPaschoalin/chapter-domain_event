using Microsoft.Extensions.Logging;
using SharedDomain.Contexts;
using SharedDomain.Models;

namespace SharedDomain.Repositories
{
    public class PersonRepository
    {
        private readonly ApplicationDbContext context;
        private readonly ILogger<PersonRepository> logger;

        public PersonRepository(ApplicationDbContext context, ILogger<PersonRepository> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public Task<PersonModel> Insert(PersonModel model)
        {
            context.Add(model);
            context.SaveChanges();
            logger.LogInformation("PersonRepository Executado. Data: {0}.", model);
            return Task.FromResult(model);
        }

        public Task<IEnumerable<PersonModel>> GetAll()
            => Task.FromResult(context.People.ToList().AsEnumerable());

        public Task<PersonModel?> GetOne(Guid id)
            => Task.FromResult(context.People.Find(id));
    }
}