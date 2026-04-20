namespace stock_service.DTOs
{
    public record UpdateProductDTO
    {
        public string Name { get; init; }
        public int Quantity { get; init; }

        public UpdateProductDTO(string name)
        {
            Name = name;
        }

    }
}
