import { Routes } from '@angular/router';

export const ESTOQUE_ROUTES: Routes = [
  {
    path: '',
    loadComponent: () =>
      import('./produto-list/produto-list.component').then(m => m.ProdutoListComponent)
  }
];
