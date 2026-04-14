# Ciclos de Vida dos Componentes Angular

O Angular disponibiliza **hooks** que permitem executar lógica em momentos específicos da vida de um componente ou diretiva.

## Ordem de execução

```
constructor()
    │
    ▼
ngOnChanges()   ← chamado sempre que um @Input muda (antes do ngOnInit e após cada mudança)
    │
    ▼
ngOnInit()      ← inicialização do componente (chamado uma única vez)
    │
    ▼
ngDoCheck()     ← detecção de mudanças customizada
    │
    ▼
ngAfterContentInit()   ← após projeção de conteúdo (ng-content)
    │
    ▼
ngAfterContentChecked()
    │
    ▼
ngAfterViewInit()      ← após a view e as views filhas serem inicializadas
    │
    ▼
ngAfterViewChecked()
    │
    ▼
ngOnDestroy()   ← limpeza antes da destruição (cancelar subscriptions, timers, etc.)
```

## Boas Práticas neste Projeto

| Hook            | Uso recomendado                                         |
|-----------------|---------------------------------------------------------|
| `ngOnInit`      | Chamar serviços para buscar dados iniciais              |
| `ngOnDestroy`   | Chamar `.unsubscribe()` ou usar `takeUntilDestroyed()`  |
| `ngOnChanges`   | Reagir a mudanças de `@Input` em componentes reutilizáveis |
| `ngAfterViewInit` | Manipular ViewChild após renderização                 |

## Gerenciamento de Subscriptions

```typescript
// Padrão recomendado com takeUntilDestroyed (Angular 16+)
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';

@Component({ ... })
export class ProdutoListComponent {
  private destroyRef = inject(DestroyRef);

  ngOnInit(): void {
    this.estoqueService.getProdutos()
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe(produtos => this.produtos = produtos);
  }
}
```
