namespace stock_service.Models
{
    public class Product
    {
        public long Id { get; private set; }
        public string Code { get; private set; }
        public string Name { get; private set; }
        public int Quantity { get; private set; }

        private Product() { } 

        public Product(string name, int initialQuantity, string code)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("O nome do produto não pode estar vazio.");

            if (initialQuantity < 0)
                throw new ArgumentException("A quantidade inicial não pode ser negativa.");

            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentException("O código sequencial é obrigatório.");

            Name = name;
            Quantity = initialQuantity;
            Code = code; 
        }

        public void AddStock(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("A quantidade para adicionar deve ser maior que zero.");

            Quantity += quantity;
        }

        public void RemoveStock(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("A quantidade para remover deve ser maior que zero.");

            if (Quantity < quantity)
                throw new InvalidOperationException($"Estoque insuficiente. Disponível: {Quantity}, Solicitado: {quantity}");

            Quantity -= quantity;
        }

        public void UpdateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("O novo nome não pode estar vazio.");

            Name = name;
        }
    }
}