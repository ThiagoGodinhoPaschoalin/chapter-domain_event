using SharedDomain.Models;
using SharedDomain.Repositories;
using WebAppWithEventDelegate.Events;

namespace WebAppWithEventDelegate.Facade
{
    public class OccurrencyService
    {
        private readonly OccurrencyRepository occurrencyRepository;
        private readonly ILogger<OccurrencyService> logger;

        public OccurrencyService(PersonService personService, OccurrencyRepository occurrencyRepository, ILogger<OccurrencyService> logger)
        {
            ///Subscriber: Aqui eu me inscrevi para o evento 'PersonCreated' 
            personService.PersonCreated += PersonCreatedHandler;
            this.occurrencyRepository = occurrencyRepository;
            this.logger = logger;
        }

        /// <summary>
        /// Esse é meu evento
        /// </summary>
        public event EventHandler<OccurrencyCreatedEventArgs>? OccurrencyCreated;

        public void PersonCreatedHandler(object? sender, PersonCreatedEventArgs args)
        {
            logger.LogInformation("Assinante OccurrencyService disparado.");
            var result = occurrencyRepository
                .Insert(new SharedDomain.Models.OccurrencyModel(SharedDomain.Models.OccurrencyType.CreatePerson, args.Model))
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();

            ///Boa prática sugerida pela Microsoft: https://docs.microsoft.com/pt-br/dotnet/standard/events/
            var @event = OccurrencyCreated;
            if (@event is null)
            {
                logger.LogInformation("Evento {EventName} é nulo, significa que nenhum assinante se inscreveu para este evento, e ele não deve ser invocado.", nameof(OccurrencyCreated));
            }
            else
            {
                ///Publisher: Aqui é a publicação do meu evento
                @event.Invoke(this, new OccurrencyCreatedEventArgs(result));
            }
        }

        public Task<IEnumerable<OccurrencyModel>> GetAll() => occurrencyRepository.GetAll();

        public Task<OccurrencyModel?> GetOne(Guid id) => occurrencyRepository.GetOne(id);
    }
}