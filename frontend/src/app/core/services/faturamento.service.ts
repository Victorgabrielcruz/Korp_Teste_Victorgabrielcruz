import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

export interface ItemNotaFiscal {
  produtoId: number;
  quantidade: number;
  precoUnitario: number;
  valorTotal: number;
}

export interface NotaFiscal {
  id: number;
  numero: string;
  serie: string;
  dataEmissao: string;
  valorTotal: number;
  status: 'RASCUNHO' | 'EMITIDA' | 'CANCELADA';
  itens: ItemNotaFiscal[];
}

export interface EmitirNotaRequest {
  serie: string;
  itens: { produtoId: number; quantidade: number }[];
}

@Injectable({ providedIn: 'root' })
export class FaturamentoService {
  private readonly baseUrl = `${environment.faturamentoApiUrl}/api/notas-fiscais`;

  constructor(private http: HttpClient) {}

  getNotasFiscais(): Observable<NotaFiscal[]> {
    return this.http.get<NotaFiscal[]>(this.baseUrl);
  }

  getNotaById(id: number): Observable<NotaFiscal> {
    return this.http.get<NotaFiscal>(`${this.baseUrl}/${id}`);
  }

  emitirNota(payload: EmitirNotaRequest): Observable<NotaFiscal> {
    return this.http.post<NotaFiscal>(this.baseUrl, payload);
  }

  cancelarNota(id: number): Observable<NotaFiscal> {
    return this.http.patch<NotaFiscal>(`${this.baseUrl}/${id}/cancelar`, {});
  }
}
