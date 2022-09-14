# HELLO WORLD #

Utilizando evento da forma mais nativa que o dotnet poderia oferecer.

Leituras importantes:

* [C#](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/)
    * [Delegates](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/delegates/)
    * [Generics](https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/types/generics)
    * [Reflection](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/reflection)
    * [generic-delegates](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/generics/generic-delegates)

* [DotNet](https://docs.microsoft.com/en-us/dotnet/fundamentals/)
    * [Events](https://docs.microsoft.com/en-us/dotnet/standard/events/)
    * [Generics](https://docs.microsoft.com/en-us/dotnet/standard/generics/)
    * [Delegates-Lambdas](https://docs.microsoft.com/en-us/dotnet/standard/delegates-lambdas)

 # Observa��es durante a implementa��o #

Agora eu registro meus assinantes logo na inicializa��o do servi�o.
Isso n�o � melhor, em desempenho, do que estava antes, mas agora eu uma tenho visibilidade maior.
Por uma quest�o de organiza��o, eu consigo fazer uma leitura mais objetiva.
```
builder.Services.AddTgpEvents(allHandlers =>
{
    allHandlers.Append<PersonCreatedEventArgs, PersonCreatedThenCreateOccurrencyHandler>();
    allHandlers.Append<PersonCreatedEventArgs, PersonCreatedThenShowLogInConsoleHandler>();
});
```

Para criar uma classe que existe para ser um assinante, siga o exemplo:
```
public class MyHandlerForXptoAction : ITgpEventHandler<PersonCreatedEventArgs>
{
    public Task Handle(object? sender, PersonCreatedEventArgs eventArgs) {
        ...///CODE HERE
    }
}
```

Perceba que eu ainda posso assinar eventos de dentro das classes, tamb�m posso ainda criar fun��es an�nimas para assinar um evento.
```
public class XptoService {
    
    public XptoService(TgpEvents events) 
    {
        //an�nimo e sem async/await
        events.Subscribe<PersonCreatedEventArgs>( (sender, eventArgs) => 
        {
            Console.WriteLine("{0}\n{1}", sender, eventArgs); 
            return Task.CompletedTask; 
        });

        //an�nimo e com async/await
        events.Subscribe<PersonCreatedEventArgs>( async (sender, eventArgs) => 
        {
            await Task.Delay(100);
            Console.WriteLine("{0}\n{1}", sender, eventArgs); 
        });

        //m�todo criado na camada de servi�o
        events.Subscribe<PersonCreatedEventArgs>( MyCustomHandler );
    }

    public Task MyCustomHandler(object? sender, PersonCreatedEventArgs eventArgs) {
        ...///Code here
    }
}
```

Detalhes da codifica��o: Em `TgpEvents` o cuidado em n�o utilizar HARD-CODE! Tudo � uma referencia.

Mas como est� organizado aqui, n�o seria bacana. Vamos fazer uma an�lise r�pida disso:

Cada vez que eu crio uma nova pessoa, est� registrando um LOG Xpto. E para este servi�o, n�o precisamos mais do LOG Xpto.
O que fazemos? Bom, de acordo com a defini��o deste exemplo, basta irmos ao StartUp, ver qual � o Evento, e na lista de Assinantes, basta deletar essa classe da rela��o.
Mas quando chego no startup, surpresa! N�o est� l�! 
Bom, ent�o espero que o amiguinho(a) desenvolvedor(a) tenha criar sua classe de Handler, na pasta padr�o do servi�o, certo?
OPS... N�o fez isso... :pensive: 
T� bem, basta dar um Ctrl+F, escrever o nome do evento, que ser� listado todos os Handlers gerado! Mas isso era preciso?

No final das contas, tudo deve ser poss�vel, pois podem existir situa��es que ainda n�o foram mapeadas, que s� poderiam ser resolvidas exatamente como no exemplo acima.