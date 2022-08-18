using SharedDomain.Models;

namespace WebAppWithEventDelegate.Events
{
    public class OccurrencyCreatedEventArgs : EventArgs
    {
        /// <summary>
        /// Evento: Ocorrência Criada.
        /// </summary>
        /// <param name="model">Não injetar o modelo diretamente! Aqui foi somente para focar na comunicação. Crie um DTO, e use AutoMapper para conversão de classe.</param>
        public OccurrencyCreatedEventArgs(OccurrencyModel model)
        {
            Model = model;
        }

        public OccurrencyModel Model { get; }
    }
}