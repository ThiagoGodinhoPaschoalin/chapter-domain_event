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

 Mudan�a que agora, ao inv�s de eu criar um "m�todo Handler" dentro do meu servi�o, eu criei uma "classe Handler", com uma interface exclusiva para isso.
 Se voc� acha que j� viu algo parecido, � pq j� leu ou implementou MediatR! ;) 
 Ao inv�s de um `INotificationHandler<INotification>`, voc� que est� utilizando System.Event, far� um `ITgpEventHandler<EventArgs>` e ter� uma expoeriencia muito parecida.

 A vantagem desta implementa��o, � a execu��o ass�ncrona. Veja o exemplo do MediatR, ver� que os Handlers s�o enfileirado e execut�dos de forma s�ncrona.
 Como o Publisher aqui funciona, ele vai abrir uma Task para cada Handler!
 Aten��o, pois isso n�o gera atomicidade!
 Se por aceso todos os Handlers necessitam finalizar com sucesso, e algum falha, n�o h� rollback neste caso!
 Existem outras observa��es aqui, tanto coisas boas quanto de aten��o, que n�o vou listar aqui, somente este que achei super importante registrar.

 Agora eu registros todos os meus assinantes l� no startup da aplica��o, ao inv�s do construtor de cada servi�o.
 Agora eu pude retirar o OccurrencyService injetado l� no PersonController. 
 
 * Baixo acoplamento entre as classes que possuem eventos.
 * Ao criar um TgpEvents com o ciclo singleton, todas as inje��es conseguir�o assinar e publicar, n�o importante o ciclo individual de cada inje��o.
     * [entenda melhor Transient, Scoped e Singleton](https://docs.microsoft.com/pt-br/dotnet/api/microsoft.extensions.dependencyinjection.servicecollectionserviceextensions?view=dotnet-plat-ext-6.0)
 * Toda regra de neg�cio dos eventos est�o encapsulados na classe TgpEvents.
 * Agora tem um Handler como UseCase, desacoplado.