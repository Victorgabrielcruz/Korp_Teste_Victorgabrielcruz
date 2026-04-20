import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../../services/product';
import { InvoiceService } from '../../../services/invoice';
import { Product } from '../../../models/product';

@Component({
  selector: 'app-invoice-form',
  templateUrl: './invoice-form.component.html',
  styleUrls: ['./invoice-form.component.scss']
})
export class InvoiceFormComponent implements OnInit {

  products: Product[] = [];

  selectedProductCode: string = '';
  quantity: number = 1;

  items: { productCode: string; quantity: number }[] = [];

  constructor(
    private productService: ProductService,
    private invoiceService: InvoiceService
  ) {}

  ngOnInit(): void {
    this.loadProducts();
  }

  loadProducts() {
    this.productService.getAll().subscribe({
      next: (data) => this.products = data,
      error: () => alert('Erro ao carregar produtos')
    });
  }

  addItem() {
    if (!this.selectedProductCode || this.quantity <= 0) {
      alert('Selecione um produto e quantidade válida');
      return;
    }

    // evita duplicar produto
    const existing = this.items.find(i => i.productCode === this.selectedProductCode);

    if (existing) {
      existing.quantity += this.quantity;
    } else {
      this.items.push({
        productCode: this.selectedProductCode,
        quantity: this.quantity
      });
    }

    this.selectedProductCode = '';
    this.quantity = 1;
  }

  removeItem(index: number) {
    this.items.splice(index, 1);
  }

  createInvoice() {
    if (this.items.length === 0) {
      alert('Adicione pelo menos um item');
      return;
    }

    const payload = {
      items: this.items
    };

    this.invoiceService.create(payload).subscribe({
      next: () => {
        alert('Nota criada com sucesso');
        this.items = [];
      },
      error: () => {
        alert('Erro ao criar nota');
      }
    });
  }

  getProductName(code: string): string {
    return this.products.find(p => p.code === code)?.name || code;
  }
}