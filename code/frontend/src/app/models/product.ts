export interface Product {
  id: number;
  code: string;
  name: string;
  quantity: number;
}
export interface CreateProduct {
  name: string;
  initialQuantity: number;
}