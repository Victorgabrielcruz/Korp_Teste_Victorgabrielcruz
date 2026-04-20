namespace stock_service.DTOs
{
    public record ResponseProductDTO
    {
        public string Code { get; init; }
        public string Name { get; init; }
        public int Quantity { get; init; }
        public long Id { get; set; }
        public ResponseProductDTO(string code, string name, int quantity, long id)
        {
            Code = code;
            Name = name;
            Quantity = quantity;
            Id = id;
        }
    }
}
