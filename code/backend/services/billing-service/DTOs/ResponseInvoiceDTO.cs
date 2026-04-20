namespace billing_service.DTOs
{
    public record ResponseInvoiceDTO
    {
        public long Id { get; init; }
        public int Number { get; init; }
        public string Status { get; init; }
        public List<ResponseInvoiceItemDTO> Items { get; init; }
        public DateTime CreatedAt { get; init; }
        public ResponseInvoiceDTO(long id, int number, string status, List<ResponseInvoiceItemDTO> items, DateTime createdAt)
        {
            Id = id;
            Number = number;
            Status = status;
            Items = items;
            CreatedAt = createdAt;
        }

        public ResponseInvoiceDTO() { }
    }
}
