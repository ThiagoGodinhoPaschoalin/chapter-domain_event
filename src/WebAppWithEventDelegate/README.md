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

 * Alto acoplamento entre as classes que possuem eventos.
 * Cuidado ao ciclo de vida do servi�o. Se for transient, o servi�o n�o ir� se inscrever.
	* [saiba mais](https://docs.microsoft.com/pt-br/dotnet/api/microsoft.extensions.dependencyinjection.servicecollectionserviceextensions?view=dotnet-plat-ext-6.0)
 * Precisa codificar cada publica��o de acordo com as boas pr�ticas