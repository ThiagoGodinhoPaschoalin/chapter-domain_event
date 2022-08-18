using MediatR;
using SharedDomain.Models;

namespace WebAppWithMediatR.CQRS.Queries
{
    /// <summary>
    /// Não injetar o modelo diretamente! Aqui foi somente para focar na comunicação. Crie um DTO, e use AutoMapper para conversão de classe.
    /// </summary>
    public class GetPersonQuery : IRequest<PersonModel?>
    {
        public GetPersonQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}