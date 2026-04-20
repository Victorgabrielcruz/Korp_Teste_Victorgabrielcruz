namespace billing_service.DTOs
{
    public record CreateInvoiceItemDTO
    {
        public string ProductCode { get; init; }
        public int Quantity { get; init; }
    }
}
