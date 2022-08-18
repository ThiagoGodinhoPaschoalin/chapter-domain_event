using MediatR;
using SharedDomain.Models;

namespace WebAppWithMediatR.CQRS.Commands
{
    public class CreateOccurrencyCommand : IRequest
    {
        /// <summary>
        /// Comando: Criar ocorrência.
        /// </summary>
        /// <param name="model">Não injetar o modelo diretamente! Aqui foi somente para focar na comunicação. Crie um DTO, e use AutoMapper para conversão de classe.</param>
        public CreateOccurrencyCommand(OccurrencyModel model)
        {
            Model = model;
        }

        public OccurrencyModel Model { get; }
    }
}