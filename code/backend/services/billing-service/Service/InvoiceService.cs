using billing_service.Data;
using billing_service.DTOs;
using billing_service.Infrastructure; // Onde está o seu StockClient
using billing_service.Models;
using billing_service.Models.Enums;
using billing_service.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace billing_service.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly ApplicationDbContext _context;
        private readonly StockClient _stockClient;

        public InvoiceService(ApplicationDbContext context, StockClient stockClient)
        {
            _context = context;
            _stockClient = stockClient;
        }

        public ResponseInvoiceDTO Create(CreateInvoiceDTO dto)
        {
            var lastNumber = _context.Invoices
                .OrderByDescending(i => i.Number)
                .Select(i => i.Number)
                .FirstOrDefault();

            var items = dto.Items
                .Select(item => new InvoiceItem(item.ProductCode, item.Quantity))
                .ToList();

            var invoice = new Invoice(lastNumber + 1, items);

            _context.Invoices.Add(invoice);
            _context.SaveChanges();

            return MapToResponseDTO(invoice);
        }

        public List<ResponseInvoiceDTO> GetAll()
        {
            return _context.Invoices
                .Include(i => i.Items)
                .OrderByDescending(i => i.CreatedAt)
                .Select(i => MapToResponseDTO(i))
                .ToList();
        }

        public ResponseInvoiceDTO Update(long id, CreateInvoiceDTO dto)
        {
            var invoice = _context.Invoices
                .Include(i => i.Items)
                .FirstOrDefault(i => i.Id == id)
                ?? throw new KeyNotFoundException("Nota Fiscal não encontrada.");

            var newItems = dto.Items
                .Select(item => new InvoiceItem(item.ProductCode, item.Quantity))
                .ToList();

            invoice.UpdateItems(newItems);
            _context.SaveChanges();

            return MapToResponseDTO(invoice);
        }

        public async Task PrintAndClose(long id)
        {
            var invoice = _context.Invoices
                .Include(i => i.Items)
                .FirstOrDefault(i => i.Id == id)
                ?? throw new KeyNotFoundException("Nota Fiscal não encontrada.");

            if (invoice.Status != InvoiceStatus.Aberta)
                throw new InvalidOperationException("Esta nota já foi fechada.");

            foreach (var item in invoice.Items)
            {
                await _stockClient.UpdateStockAsync(item.ProductCode, item.Quantity);
            }

            invoice.MarkAsClosed();
            await _context.SaveChangesAsync();
        }
        private static ResponseInvoiceDTO MapToResponseDTO(Invoice invoice)
        {
            return new ResponseInvoiceDTO(
                invoice.Id,
                invoice.Number,
                invoice.Status.ToString(),
                invoice.Items.Select(it => new ResponseInvoiceItemDTO(it.ProductCode, it.Quantity)).ToList(),
                invoice.CreatedAt
            );
        }
    }
}