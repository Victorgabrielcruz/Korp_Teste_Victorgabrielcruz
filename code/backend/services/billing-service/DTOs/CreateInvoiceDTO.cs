namespace billing_service.DTOs
{
    public record CreateInvoiceDTO
    {
        public List<CreateInvoiceItemDTO> Items { get; init; }
    }
}
