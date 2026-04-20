using stock_service.Data;
using stock_service.Models;
using stock_service.DTOs;
using stock_service.Services.Interfaces;

namespace stock_service.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        public ResponseProductDTO Create(CreateProductDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException("O nome do produto é obrigatório.");

            var nextCode = GenerateNextCode();
            var product = new Product(dto.Name, dto.InitialQuantity, nextCode);

            _context.Products.Add(product);
            _context.SaveChanges();

            return new ResponseProductDTO(product.Code, product.Name, product.Quantity, product.Id);
        }

        public List<ResponseProductDTO> GetAll()
        {
            return _context.Products
                .Select(p => new ResponseProductDTO(p.Code, p.Name, p.Quantity, p.Id))
                .ToList();
        }

        public ResponseProductDTO GetById(long id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id)
                ?? throw new KeyNotFoundException($"Produto com ID {id} não encontrado.");

            return new ResponseProductDTO(product.Code, product.Name, product.Quantity, product.Id);
        }

        public ResponseProductDTO GetByCode(string code)
        {
            var product = _context.Products.FirstOrDefault(p => p.Code == code)
                ?? throw new KeyNotFoundException($"Produto com código {code} não encontrado.");

            return new ResponseProductDTO(product.Code, product.Name, product.Quantity, product.Id);
        }

        public ResponseProductDTO UpdateProduct(long id, UpdateProductDTO dto)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id)
                ?? throw new KeyNotFoundException("Produto não encontrado para atualização.");
            if (dto.Quantity > 0)
                product.AddStock(dto.Quantity);
            else
                product.RemoveStock(-dto.Quantity);

            product.UpdateName(dto.Name);
            _context.SaveChanges();

            return new ResponseProductDTO(product.Code, product.Name, product.Quantity, product.Id);
        }

        public void UpdateStock(long id, int quantity)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id)
                ?? throw new KeyNotFoundException("Produto não encontrado.");

            if (quantity > 0)
                product.AddStock(quantity);
            else
                product.RemoveStock(-quantity);

            _context.SaveChanges();
        }

        public void Delete(long id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id)
                ?? throw new KeyNotFoundException("Produto não encontrado para exclusão.");

            _context.Products.Remove(product);
            _context.SaveChanges();
        }

        private string GenerateNextCode()
        {
            var lastProduct = _context.Products
                .Where(p => p.Code.StartsWith("P"))
                .OrderByDescending(p => p.Code)
                .FirstOrDefault();

            int nextNumber = 1;

            if (lastProduct != null && lastProduct.Code.Length > 1)
            {
                if (int.TryParse(lastProduct.Code.Substring(1), out int numberPart))
                {
                    nextNumber = numberPart + 1;
                }
            }

            return $"P{nextNumber:D5}";
        }

        public bool HasAvailableStock(string code, int quantity)
        {
            var product = _context.Products.FirstOrDefault(p => p.Code == code);
            return product != null && product.Quantity >= quantity;
        }
    }
}