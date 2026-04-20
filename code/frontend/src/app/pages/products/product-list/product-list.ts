import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../../services/product';
import { Product } from '../../../models/product';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html'
})
export class ProductList implements OnInit {

  products: Product[] = [];

  constructor(private productService: ProductService) {}

  ngOnInit(): void {
    this.loadProducts();
  }

  loadProducts() {
    this.productService.getAll().subscribe({
      next: (data) => this.products = data,
      error: () => alert('Erro ao carregar produtos')
    });
  }
}