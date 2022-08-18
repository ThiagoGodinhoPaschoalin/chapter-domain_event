using MediatR;

namespace WebAppWithMediatR.CQRS.Commands
{
    public class CreatePersonCommand : IRequest<Guid>
    {
        /// <summary>
        /// Comando: Criar pessoa.
        /// </summary>
        /// <param name="name">nome da pessoa</param>
        public CreatePersonCommand(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }

        public Guid Id { get; }
        public string Name { get; }
    }
}