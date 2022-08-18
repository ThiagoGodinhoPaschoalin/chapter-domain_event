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

 A única diferença neste exemplo, é que foi criado uma classe TgpEvents para abstrair o processo dos eventos.
 Agora, uma única classe cuida para assinar e publicar os eventos existentes, que são gerados em tempo de execução.

 * Baixo acoplamento entre as classes que possuem eventos.
 * Ao criar um TgpEvents com o ciclo singleton, todas as injeções conseguirão assinar e publicar, não importante o ciclo individual de cada injeção.
     * [entenda melhor Transient, Scoped e Singleton](https://docs.microsoft.com/pt-br/dotnet/api/microsoft.extensions.dependencyinjection.servicecollectionserviceextensions?view=dotnet-plat-ext-6.0)
 * Toda regra de negócio dos eventos estão encapsulados na classe TgpEvents.
 * Não preciso mais dar manutenção aos Handlers.