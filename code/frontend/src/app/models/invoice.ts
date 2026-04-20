export interface CreateInvoiceItem {
  productCode: string;
  quantity: number;
}

export interface CreateInvoice {
  items: CreateInvoiceItem[];
}
export interface InvoiceItem {
  productCode: string;
  quantity: number;
}

export interface Invoice {
  id: number;
  number: number;
  status: string;
  items: InvoiceItem[];
  createdAt: string;
}