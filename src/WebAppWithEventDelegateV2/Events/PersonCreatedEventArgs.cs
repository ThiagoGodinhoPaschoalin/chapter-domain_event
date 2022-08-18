using SharedDomain.Models;

namespace WebAppWithEventDelegateByGodinho.Events
{
    public class PersonCreatedEventArgs : EventArgs
    {
        /// <summary>
        /// Evento: Pessoa Criada.
        /// </summary>
        /// <param name="model">Não injetar o modelo diretamente! Aqui foi somente para focar na comunicação. Crie um DTO, e use AutoMapper para conversão de classe.</param>
        public PersonCreatedEventArgs(PersonModel model)
        {
            Model = model;
        }

        public PersonModel Model { get; }
    }
}