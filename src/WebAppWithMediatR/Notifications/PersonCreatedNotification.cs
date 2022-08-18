using MediatR;
using SharedDomain.Models;

namespace WebAppWithMediatR.Notifications
{
    public class PersonCreatedNotification : INotification
    {
        /// <summary>
        /// Notificação: Pessoa criada!
        /// </summary>
        /// <param name="model">Não injetar o modelo diretamente! Aqui foi somente para focar na comunicação. Crie um DTO, e use AutoMapper para conversão de classe.</param>
        public PersonCreatedNotification(PersonModel model)
        {
            Model = model;
        }

        public PersonModel Model { get; }
    }
}