using SharedDomain.Models;
using SharedDomain.Repositories;
using WebAppWithReflectionAndEventDelegate.Events;

namespace WebAppWithReflectionAndEventDelegate.Facade
{
    public class PersonService
    {
        private readonly PersonRepository personRepository;
        private readonly TgpEvents events;

        public PersonService(PersonRepository personRepository, TgpEvents events)
        {
            this.personRepository = personRepository;
            this.events = events;
        }

        public async Task<Guid> Insert(PersonModel personModel)
        {
            var result = await personRepository.Insert(personModel);

            if (result is null)
                throw new AggregateException($"blablabla, deu erro ao tentar persistir uma nova pessoa no banco de dados.");

            ///Perceba que não preciso mais:
            ///* criar o 'public event EventHandler<PersonCreatedEventArgs>? PersonCreated';
            ///* Gerar regras para validar a existencia de assinantes;
            events.Publish(new PersonCreatedEventArgs(result));

            return result.Id;
        }

        public Task<PersonModel?> GetOne(Guid id) => personRepository.GetOne(id);

        public Task<IEnumerable<PersonModel>> GetAll() => personRepository.GetAll();
    }
}