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

Perceba que eu ainda posso assinar eventos de dentro das classes, tamb�m posso ainda criar fun��es an�nimas para assinar um evento.

Mas como est� organizado aqui, n�o seria bacana. Vamos fazer uma an�lise r�pida disso:

Cada vez que eu crio uma nova pessoa, est� registrando um LOG Xpto. E para este servi�o, n�o precisamos mais do LOG Xpto.
O que fazemos? Bom, de acordo com a defini��o deste exemplo, basta irmos ao StartUp, ver qual � o Evento, e na lista de Assinantes, basta deletar essa classe da rela��o.
Mas quando chego no startup, surpresa! N�o est� l�! 
Bom, ent�o espero que o amiguinho(a) desenvolvedor(a) tenha criar sua classe de Handler, na pasta padr�o do servi�o, certo?
OPS... N�o fez isso... :pensive: 
T� bem, basta dar um Ctrl+F, escrever o nome do evento, que ser� listado todos os Handlers gerado! Mas isso era preciso?