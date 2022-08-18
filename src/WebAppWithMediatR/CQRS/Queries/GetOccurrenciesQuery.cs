using MediatR;
using SharedDomain.Models;

namespace WebAppWithMediatR.CQRS.Queries
{
    /// <summary>
    /// Não injetar o modelo diretamente! Aqui foi somente para focar na comunicação. Crie um DTO, e use AutoMapper para conversão de classe.
    /// </summary>
    public class GetOccurrenciesQuery : IRequest<IEnumerable<OccurrencyModel>>
    { }
}