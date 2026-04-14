import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

export interface Produto {
  id: number;
  codigo: string;
  descricao: string;
  unidadeMedida: string;
  saldo: number;
  precoUnitario: number;
}

@Injectable({ providedIn: 'root' })
export class EstoqueService {
  private readonly baseUrl = `${environment.estoqueApiUrl}/api/produtos`;

  constructor(private http: HttpClient) {}

  getProdutos(): Observable<Produto[]> {
    return this.http.get<Produto[]>(this.baseUrl);
  }

  getProdutoById(id: number): Observable<Produto> {
    return this.http.get<Produto>(`${this.baseUrl}/${id}`);
  }

  createProduto(produto: Omit<Produto, 'id'>): Observable<Produto> {
    return this.http.post<Produto>(this.baseUrl, produto);
  }

  updateProduto(id: number, produto: Partial<Produto>): Observable<Produto> {
    return this.http.put<Produto>(`${this.baseUrl}/${id}`, produto);
  }

  deleteProduto(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }
}
