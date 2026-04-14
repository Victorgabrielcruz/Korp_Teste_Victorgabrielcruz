import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'estoque',
    pathMatch: 'full'
  },
  {
    path: 'estoque',
    loadChildren: () =>
      import('./features/estoque/estoque.routes').then(m => m.ESTOQUE_ROUTES)
  },
  {
    path: 'notas-fiscais',
    loadChildren: () =>
      import('./features/notas-fiscais/notas-fiscais.routes').then(m => m.NOTAS_FISCAIS_ROUTES)
  }
];
