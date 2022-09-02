# HELLO WORLD #

Utilizando evento da forma mais nativa que o dotnet poderia oferecer.

Leituras importantes:

* [dotnet events](https://docs.microsoft.com/pt-br/dotnet/standard/events/)
* [dotnet multiple events](https://docs.microsoft.com/pt-br/dotnet/standard/events/how-to-handle-multiple-events-using-event-properties)
* [architecture microservices](https://docs.microsoft.com/pt-br/dotnet/architecture/microservices/)

Extra:
* https://docs.microsoft.com/pt-br/dotnet/architecture/microservices/multi-container-microservice-net-applications/
* https://docs.microsoft.com/pt-br/dotnet/api/system.threading.eventwaithandle?view=net-6.0

 # Observações durante a implementação #

 Mudança que agora, ao invés de eu criar um "método Handler" dentro do meu serviço, eu criei uma "classe Handler", com uma interface exclusiva para isso.
 Se você acha que já viu algo parecido, é pq já leu ou implementou MediatR! ;) 
 Ao invés de um `INotificationHandler<INotification>`, você que está utilizando System.Event, fará um `ITgpEventHandler<EventArgs>` e terá uma expoeriencia muito parecida.

 A vantagem desta implementação, é a execução assíncrona. Veja o exemplo do MediatR, verá que os Handlers são enfileirado e executádos de forma síncrona.
 Como o Publisher aqui funciona, ele vai abrir uma Task para cada Handler!
 Atenção, pois isso não gera atomicidade!
 Se por aceso todos os Handlers necessitam finalizar com sucesso, e algum falha, não há rollback neste caso!
 Existem outras observações aqui, tanto coisas boas quanto de atenção, que não vou listar aqui, somente este que achei super importante registrar.

 Agora eu registros todos os meus assinantes lá no startup da aplicação, ao invés do construtor de cada serviço.
 Agora eu pude retirar o OccurrencyService injetado lá no PersonController. 
 
 * Baixo acoplamento entre as classes que possuem eventos.
 * Ao criar um TgpEvents com o ciclo singleton, todas as injeções conseguirão assinar e publicar, não importante o ciclo individual de cada injeção.
     * [entenda melhor Transient, Scoped e Singleton](https://docs.microsoft.com/pt-br/dotnet/api/microsoft.extensions.dependencyinjection.servicecollectionserviceextensions?view=dotnet-plat-ext-6.0)
 * Toda regra de negócio dos eventos estão encapsulados na classe TgpEvents.
 * Agora tem um Handler como UseCase, desacoplado.