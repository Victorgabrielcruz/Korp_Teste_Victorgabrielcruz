# Padrões RxJS utilizados no Projeto

## Operadores principais

### `switchMap` – Chamadas HTTP encadeadas

Utilizado quando a resposta de uma observable deve disparar outra requisição, cancelando a anterior.

```typescript
this.searchControl.valueChanges.pipe(
  debounceTime(300),
  distinctUntilChanged(),
  switchMap(termo => this.estoqueService.buscarProdutos(termo))
).subscribe(produtos => this.produtos = produtos);
```

### `combineLatest` – Dados combinados de múltiplas fontes

```typescript
combineLatest([
  this.estoqueService.getProdutos(),
  this.faturamentoService.getNotasFiscais()
]).subscribe(([produtos, notas]) => {
  this.dashboard = { produtos, notas };
});
```

### `forkJoin` – Múltiplas requisições paralelas, aguardando todas

```typescript
forkJoin({
  produtos: this.estoqueService.getProdutos(),
  clientes: this.clienteService.getClientes()
}).subscribe(({ produtos, clientes }) => {
  // todos os dados disponíveis
});
```

### `catchError` – Tratamento de erros

```typescript
this.faturamentoService.emitirNota(payload).pipe(
  catchError(err => {
    this.notificationService.error(err.message);
    return EMPTY;
  })
).subscribe(...);
```

### `shareReplay` – Cache de requisições

```typescript
private produtos$ = this.http.get<Produto[]>('/api/produtos').pipe(
  shareReplay(1)
);
```

## Subjects utilizados

| Subject         | Caso de uso                                      |
|-----------------|--------------------------------------------------|
| `BehaviorSubject` | Estado global (ex.: carrinho, usuário logado)  |
| `Subject`        | Eventos pontuais (ex.: fechar modal)            |
| `ReplaySubject`  | Histórico de eventos (ex.: notificações)        |
