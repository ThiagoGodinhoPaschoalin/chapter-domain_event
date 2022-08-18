using MediatR;
using SharedDomain.Models;

namespace WebAppWithMediatR.CQRS.Queries
{
    /// <summary>
    /// Não injetar o modelo diretamente! Aqui foi somente para focar na comunicação. Crie um DTO, e use AutoMapper para conversão de classe.
    /// </summary>
    public class GetOccurrencyQuery : IRequest<OccurrencyModel?>
    {
        public GetOccurrencyQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}