import { Component, OnInit } from '@angular/core';
import { InvoiceService } from '../../../services/invoice';

@Component({
  selector: 'app-invoice-list',
  templateUrl: './invoice-list.component.html',
  styleUrls: ['./invoice-list.component.scss']
})
export class InvoiceListComponent implements OnInit {

  invoices: any[] = [];
  loadingId: number | null = null;

  constructor(private invoiceService: InvoiceService) {}

  ngOnInit(): void {
    this.loadInvoices();
  }

  loadInvoices() {
    this.invoiceService.getAll().subscribe({
      next: (data) => this.invoices = data,
      error: () => alert('Erro ao carregar notas')
    });
  }

  print(id: number) {
    this.loadingId = id;

    this.invoiceService.print(id).subscribe({
      next: () => {
        this.loadingId = null;
        this.loadInvoices(); // atualiza lista (status muda)
      },
      error: () => {
        this.loadingId = null;
        alert('Erro ao imprimir nota');
      }
    });
  }
}