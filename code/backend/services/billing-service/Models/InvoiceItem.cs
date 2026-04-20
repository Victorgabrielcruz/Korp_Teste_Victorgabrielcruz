namespace billing_service.Models
{
    public class InvoiceItem
    {
        public long Id { get; private set; }
        public string ProductCode { get; private set; } 
        public int Quantity { get; private set; } 
        public long InvoiceId { get; private set; }

        private InvoiceItem() { }

        public InvoiceItem(string productCode, int quantity)
        {
            if (string.IsNullOrWhiteSpace(productCode))
                throw new ArgumentException("O código do produto é obrigatório.");

            if (quantity <= 0)
                throw new ArgumentException("A quantidade deve ser maior que zero.");

            ProductCode = productCode;
            Quantity = quantity;
        }
    }
}