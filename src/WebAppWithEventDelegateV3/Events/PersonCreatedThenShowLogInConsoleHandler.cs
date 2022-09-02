namespace WebAppWithEventDelegateV3.Events
{
    public class PersonCreatedThenShowLogInConsoleHandler : ITgpEventHandler<PersonCreatedEventArgs>
    {
        private readonly ILogger<PersonCreatedThenShowLogInConsoleHandler> logger;

        public PersonCreatedThenShowLogInConsoleHandler(ILogger<PersonCreatedThenShowLogInConsoleHandler> logger)
        {
            this.logger = logger;
        }

        public Task Handle(object sender, PersonCreatedEventArgs eventArgs)
        {
            logger.LogWarning("Estou aqui somente para mostrar um log no console!");
            return Task.CompletedTask;
        }
    }
}