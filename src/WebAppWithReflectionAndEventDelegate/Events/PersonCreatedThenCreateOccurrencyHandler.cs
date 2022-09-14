using SharedDomain.Models;
using SharedDomain.Repositories;

namespace WebAppWithReflectionAndEventDelegate.Events
{
    public class PersonCreatedThenCreateOccurrencyHandler : ITgpEventHandler<PersonCreatedEventArgs>
    {
        private readonly OccurrencyRepository occurrencyRepository;
        private readonly ILogger<PersonCreatedThenCreateOccurrencyHandler> logger;

        public PersonCreatedThenCreateOccurrencyHandler(OccurrencyRepository occurrencyRepository, ILogger<PersonCreatedThenCreateOccurrencyHandler> logger)
        {
            this.occurrencyRepository = occurrencyRepository;
            this.logger = logger;
        }

        public Task Handle(object? sender, PersonCreatedEventArgs eventArgs)
        {
            logger.LogInformation("Assinante PersonCreatedThenCreateOccurrencyHandler disparado.");
            return occurrencyRepository.Insert(new OccurrencyModel(OccurrencyType.CreatePerson, eventArgs.Model));
        }
    }
}