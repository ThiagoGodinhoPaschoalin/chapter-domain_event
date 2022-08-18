# HELLO WORLD #

Utilizando evento da forma mais nativa que o dotnet poderia oferecer.

Leituras importantes:

* [dotnet events](https://docs.microsoft.com/pt-br/dotnet/standard/events/)
* [dotnet multiple events](https://docs.microsoft.com/pt-br/dotnet/standard/events/how-to-handle-multiple-events-using-event-properties)
* [architecture microservices](https://docs.microsoft.com/pt-br/dotnet/architecture/microservices/)

Extra:
* https://docs.microsoft.com/pt-br/dotnet/architecture/microservices/multi-container-microservice-net-applications/
* https://docs.microsoft.com/pt-br/dotnet/api/system.threading.eventwaithandle?view=net-6.0

 # Observa��es durante a implementa��o #

 A �nica diferen�a neste exemplo, � que foi criado uma classe TgpEvents para abstrair o processo dos eventos.
 Agora, uma �nica classe cuida para assinar e publicar os eventos existentes, que s�o gerados em tempo de execu��o.

 * Baixo acoplamento entre as classes que possuem eventos.
 * Ao criar um TgpEvents com o ciclo singleton, todas as inje��es conseguir�o assinar e publicar, n�o importante o ciclo individual de cada inje��o.
     * [entenda melhor Transient, Scoped e Singleton](https://docs.microsoft.com/pt-br/dotnet/api/microsoft.extensions.dependencyinjection.servicecollectionserviceextensions?view=dotnet-plat-ext-6.0)
 * Toda regra de neg�cio dos eventos est�o encapsulados na classe TgpEvents.
 * N�o preciso mais dar manuten��o aos Handlers.