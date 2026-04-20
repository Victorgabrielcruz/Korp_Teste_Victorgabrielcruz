import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CreateInvoice, Invoice } from '../models/invoice';
import { Observable } from 'rxjs/internal/Observable';

@Injectable({
  providedIn: 'root'
})
export class InvoiceService {

  private api = 'http://localhost:5001/invoices';

  constructor(private http: HttpClient) {}

  getAll(): Observable<Invoice[]> {
    return this.http.get<Invoice[]>(this.api);
  }

  create(invoice: CreateInvoice): Observable<Invoice> {
    return this.http.post<Invoice>(this.api, invoice);
  }

  print(id: number) {
    return this.http.post(`${this.api}/${id}/print`, {});
  }
}