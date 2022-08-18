using SharedDomain.Models;
using SharedDomain.Repositories;
using WebAppWithEventDelegate.Events;

namespace WebAppWithEventDelegate.Facade
{
    public class PersonService
    {
        private readonly PersonRepository personRepository;
        private readonly ILogger<PersonService> logger;

        public PersonService(PersonRepository personRepository, ILogger<PersonService> logger)
        {
            this.personRepository = personRepository;
            this.logger = logger;
        }

        public event EventHandler<PersonCreatedEventArgs>? PersonCreated;

        public async Task<Guid> Insert(PersonModel personModel)
        {
            var result = await personRepository.Insert(personModel);

            if (result is null)
                throw new AggregateException($"blablabla, deu erro ao tentar persistir uma nova pessoa no banco de dados.");

            ///Boa prática sugerida pela Microsoft: https://docs.microsoft.com/pt-br/dotnet/standard/events/
            var @event = PersonCreated;
            if (@event is null)
            {
                logger.LogInformation("Evento {EventName} é nulo, significa que nenhum assinante se inscreveu para este evento, e ele não deve ser invocado.", nameof(PersonCreated));
            }
            else
            {
                @event.Invoke(this, new PersonCreatedEventArgs(result));
            }

            return result.Id;
        }

        public Task<PersonModel?> GetOne(Guid id) => personRepository.GetOne(id);

        public Task<IEnumerable<PersonModel>> GetAll() => personRepository.GetAll();
    }
}