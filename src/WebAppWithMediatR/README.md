# HELLO WORLD

Utilizando a biblioteca MediatR para abstrair a complexidade dos padrões CQRS e Notification.

 # Observações durante a implementação

 Curva de aprenzidado super baixa, além de ser uma implementação super simples para iniciar e testar.

 Com facilidade eu posso criar vários Handlers assinados em uma Notificação, e assim executar várias funções com baixissímo acoplamento, já que existe a necessidade de que ambos os lados conheçam a classe de Notificação.

 A biblioteca se encarrega do discovery do serviço.

 ## Limitação encontrada:
 A execução dos N Handlers é feita de forma SÍNCRONA!

 O que pode afetar bastante o tempo de resposta.

 Exemplo:
```
public class FazIssoHandler : INotificationHandler<FoiFeitoIssoNotication>
{
	public async Task Handle(FoiFeitoIssoNotication notification, CancellationToken cancellationToken)
	{
		await Task.Delay(5000);
	}
}

public class FazIssoTambemHandler : INotificationHandler<FoiFeitoIssoNotication>
{
	public async Task Handle(FoiFeitoIssoNotication notification, CancellationToken cancellationToken)
	{
		await Task.Delay(5000);
	}
}
```

Esse código acima vai demorar 10 segundos para concluir.