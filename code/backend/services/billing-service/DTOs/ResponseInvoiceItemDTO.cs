namespace billing_service.DTOs
{
    public record ResponseInvoiceItemDTO
    {
        public string ProductCode { get; init; }
        public int Quantity { get; init; }

        public ResponseInvoiceItemDTO() { }

        public ResponseInvoiceItemDTO(string productCode, int quantity)
        {
            ProductCode = productCode;
            Quantity = quantity;
        }
    }
}