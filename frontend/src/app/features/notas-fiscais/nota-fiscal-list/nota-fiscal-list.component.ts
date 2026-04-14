import { Component, OnInit, inject, DestroyRef } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatChipsModule } from '@angular/material/chips';
import { FaturamentoService, NotaFiscal } from '../../../core/services/faturamento.service';

@Component({
  selector: 'app-nota-fiscal-list',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatProgressSpinnerModule, MatChipsModule],
  template: `
    <h2>Notas Fiscais</h2>
    <mat-spinner *ngIf="loading" diameter="40"></mat-spinner>
    <table mat-table [dataSource]="notas" *ngIf="!loading">
      <ng-container matColumnDef="numero">
        <th mat-header-cell *matHeaderCellDef>Número</th>
        <td mat-cell *matCellDef="let n">{{ n.numero }}</td>
      </ng-container>
      <ng-container matColumnDef="serie">
        <th mat-header-cell *matHeaderCellDef>Série</th>
        <td mat-cell *matCellDef="let n">{{ n.serie }}</td>
      </ng-container>
      <ng-container matColumnDef="dataEmissao">
        <th mat-header-cell *matHeaderCellDef>Data Emissão</th>
        <td mat-cell *matCellDef="let n">{{ n.dataEmissao | date:'dd/MM/yyyy HH:mm' }}</td>
      </ng-container>
      <ng-container matColumnDef="valorTotal">
        <th mat-header-cell *matHeaderCellDef>Valor Total</th>
        <td mat-cell *matCellDef="let n">{{ n.valorTotal | currency:'BRL' }}</td>
      </ng-container>
      <ng-container matColumnDef="status">
        <th mat-header-cell *matHeaderCellDef>Status</th>
        <td mat-cell *matCellDef="let n">
          <mat-chip [color]="n.status === 'EMITIDA' ? 'primary' : n.status === 'CANCELADA' ? 'warn' : 'accent'">
            {{ n.status }}
          </mat-chip>
        </td>
      </ng-container>
      <tr mat-header-row *matHeaderRowDef="displayedColumns"></tr>
      <tr mat-row *matRowDef="let row; columns: displayedColumns;"></tr>
    </table>
  `
})
export class NotaFiscalListComponent implements OnInit {
  private faturamentoService = inject(FaturamentoService);
  private destroyRef = inject(DestroyRef);

  notas: NotaFiscal[] = [];
  loading = true;
  displayedColumns = ['numero', 'serie', 'dataEmissao', 'valorTotal', 'status'];

  ngOnInit(): void {
    this.faturamentoService.getNotasFiscais()
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: notas => { this.notas = notas; this.loading = false; },
        error: () => { this.loading = false; }
      });
  }
}
