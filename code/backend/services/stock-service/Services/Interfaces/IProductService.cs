using stock_service.DTOs;

namespace stock_service.Services.Interfaces
{
    public interface IProductService
    {
        ResponseProductDTO Create(CreateProductDTO dto);
        List<ResponseProductDTO> GetAll();
        ResponseProductDTO GetById(long id);
        ResponseProductDTO GetByCode(string code);
        ResponseProductDTO UpdateProduct(long id, UpdateProductDTO dto);
        void UpdateStock(long id, int quantity);
        void Delete(long id);

        bool HasAvailableStock(string code, int quantity);
    }
}