using SharedDomain.Models;
using SharedDomain.Repositories;
using WebAppWithEventDelegateByGodinho.Events;

namespace WebAppWithEventDelegateByGodinho.Facade
{
    /* Perceba agora que:
     * * Não preciso mais injetar o(s) serviço(s) de onde eles devem ser publicados (PersonService).
     * * Posso me inscrever sem precisar conhecer o publicador (PersonService), basta informar qual evento (PersonCreatedEventArgs) desejo me inscrever.
     * * Meu handler agora é assíncrono (PersonCreatedHandler).
     * * Não preciso mais criar eventos (public event EventHandler...) nas classes onde gostaria de publicar eventos.
     */

    public class OccurrencyService
    {
        private readonly TgpEvents events;
        private readonly OccurrencyRepository occurrencyRepository;
        private readonly ILogger<OccurrencyService> logger;

        public OccurrencyService(TgpEvents events, OccurrencyRepository occurrencyRepository, ILogger<OccurrencyService> logger)
        {
            this.events = events;
            ///Subscriber: Aqui eu me inscrevi para o evento 'PersonCreated' 
            this.events.Subscribe<PersonCreatedEventArgs>(PersonCreatedHandler);

            this.occurrencyRepository = occurrencyRepository;
            this.logger = logger;
        }

        ///Perceba que, agora posso criar meu Handler assíncrono;
        public async Task PersonCreatedHandler(object? sender, PersonCreatedEventArgs args)
        {
            logger.LogInformation("Assinante OccurrencyService disparado.");
            var result = await occurrencyRepository.Insert(new OccurrencyModel(OccurrencyType.CreatePerson, args.Model));

            events.Publish(new OccurrencyCreatedEventArgs(result));
        }

        public Task<IEnumerable<OccurrencyModel>> GetAll() => occurrencyRepository.GetAll();

        public Task<OccurrencyModel?> GetOne(Guid id) => occurrencyRepository.GetOne(id);
    }
}