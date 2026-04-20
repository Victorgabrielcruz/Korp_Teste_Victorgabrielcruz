import { Component } from '@angular/core';
import { ProductService } from '../../../services/product';

@Component({
  selector: 'app-product-form',
  templateUrl: './product-form.component.html'
})
export class ProductForm {

  product = {
    name: '',
    initialQuantity: 0
  };

  constructor(private productService: ProductService) {}

  save() {
    this.productService.create(this.product).subscribe({
      next: () => {
        alert('Produto criado com sucesso');
        this.product = { name: '', initialQuantity: 0 };
      },
      error: () => {
        alert('Erro ao criar produto');
      }
    });
  }
}