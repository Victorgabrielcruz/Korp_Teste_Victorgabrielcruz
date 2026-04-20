using billing_service.DTOs;

namespace billing_service.Service.Interfaces
{
    public interface IInvoiceService
    {
        ResponseInvoiceDTO Create(CreateInvoiceDTO dto);
        List<ResponseInvoiceDTO> GetAll();
        Task PrintAndClose(long id);

        ResponseInvoiceDTO Update(long id, CreateInvoiceDTO dto);
    }
}
