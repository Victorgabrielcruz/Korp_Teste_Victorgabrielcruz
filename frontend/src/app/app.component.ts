import { Component } from '@angular/core';
import { RouterOutlet, RouterLink, RouterLinkActive } from '@angular/router';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, RouterLink, RouterLinkActive, MatToolbarModule, MatButtonModule],
  template: `
    <mat-toolbar color="primary">
      <span>Sistema de Notas Fiscais</span>
      <span style="flex: 1"></span>
      <a mat-button routerLink="/estoque" routerLinkActive="active-link">Estoque</a>
      <a mat-button routerLink="/notas-fiscais" routerLinkActive="active-link">Notas Fiscais</a>
    </mat-toolbar>
    <main style="padding: 24px">
      <router-outlet />
    </main>
  `,
  styles: [`
    .active-link { background: rgba(255,255,255,0.15); border-radius: 4px; }
  `]
})
export class AppComponent {
  title = 'Sistema de Notas Fiscais';
}
