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

 # Observações durante a implementação #

Agora eu registro meus assinantes logo na inicialização do serviço.
Isso não é melhor, em desempenho, do que estava antes, mas agora eu uma tenho visibilidade maior.
Por uma questão de organização, eu consigo fazer uma leitura mais objetiva.

Perceba que eu ainda posso assinar eventos de dentro das classes, também posso ainda criar funções anônimas para assinar um evento.

Mas como está organizado aqui, não seria bacana. Vamos fazer uma análise rápida disso:

Cada vez que eu crio uma nova pessoa, está registrando um LOG Xpto. E para este serviço, não precisamos mais do LOG Xpto.
O que fazemos? Bom, de acordo com a definição deste exemplo, basta irmos ao StartUp, ver qual é o Evento, e na lista de Assinantes, basta deletar essa classe da relação.
Mas quando chego no startup, surpresa! Não está lá! 
Bom, então espero que o amiguinho(a) desenvolvedor(a) tenha criar sua classe de Handler, na pasta padrão do serviço, certo?
OPS... Não fez isso... :pensive: 
Tá bem, basta dar um Ctrl+F, escrever o nome do evento, que será listado todos os Handlers gerado! Mas isso era preciso?