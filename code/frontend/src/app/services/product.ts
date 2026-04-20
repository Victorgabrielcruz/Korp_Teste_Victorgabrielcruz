import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Product } from '../models/product';
import { CreateProduct } from '../models/product';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  private api = 'http://localhost:5000/products';

  constructor(private http: HttpClient) {}

  getAll(): Observable<Product[]> {
    return this.http.get<Product[]>(this.api);
  }

  create(product: CreateProduct): Observable<Product> {
    return this.http.post<Product>(this.api, product);
  }
}