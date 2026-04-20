namespace stock_service.DTOs
{
    public record CreateProductDTO
    {
        public string Name { get; init; }
        public int InitialQuantity { get; init; }
    }
}
