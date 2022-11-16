# Todo.Consumer

Representam um serviço que consome mensagens de uma fila e as processa.

## Como executar

Trata-se de um Console Application, basta entrar no diretório e executar:

```
dotnet run
``` 

A medida que nossa aplicação TodoApp realiza as operações de domínio, eventos são gerados e podemos ver esses eventos no console deste consumidor.