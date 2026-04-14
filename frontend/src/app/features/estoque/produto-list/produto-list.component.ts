import { Component, OnInit, inject, DestroyRef } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatButtonModule } from '@angular/material/button';
import { EstoqueService, Produto } from '../../../core/services/estoque.service';

@Component({
  selector: 'app-produto-list',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatProgressSpinnerModule, MatButtonModule],
  template: `
    <h2>Produtos em Estoque</h2>
    <mat-spinner *ngIf="loading" diameter="40"></mat-spinner>
    <table mat-table [dataSource]="produtos" *ngIf="!loading">
      <ng-container matColumnDef="codigo">
        <th mat-header-cell *matHeaderCellDef>Código</th>
        <td mat-cell *matCellDef="let p">{{ p.codigo }}</td>
      </ng-container>
      <ng-container matColumnDef="descricao">
        <th mat-header-cell *matHeaderCellDef>Descrição</th>
        <td mat-cell *matCellDef="let p">{{ p.descricao }}</td>
      </ng-container>
      <ng-container matColumnDef="saldo">
        <th mat-header-cell *matHeaderCellDef>Saldo</th>
        <td mat-cell *matCellDef="let p">{{ p.saldo }} {{ p.unidadeMedida }}</td>
      </ng-container>
      <ng-container matColumnDef="preco">
        <th mat-header-cell *matHeaderCellDef>Preço Unit.</th>
        <td mat-cell *matCellDef="let p">{{ p.precoUnitario | currency:'BRL' }}</td>
      </ng-container>
      <tr mat-header-row *matHeaderRowDef="displayedColumns"></tr>
      <tr mat-row *matRowDef="let row; columns: displayedColumns;"></tr>
    </table>
  `
})
export class ProdutoListComponent implements OnInit {
  private estoqueService = inject(EstoqueService);
  private destroyRef = inject(DestroyRef);

  produtos: Produto[] = [];
  loading = true;
  displayedColumns = ['codigo', 'descricao', 'saldo', 'preco'];

  ngOnInit(): void {
    this.estoqueService.getProdutos()
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: produtos => { this.produtos = produtos; this.loading = false; },
        error: () => { this.loading = false; }
      });
  }
}
