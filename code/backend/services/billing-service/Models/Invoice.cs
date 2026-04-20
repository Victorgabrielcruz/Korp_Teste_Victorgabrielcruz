using billing_service.Models.Enums;

namespace billing_service.Models
{
    public class Invoice
    {
        public long Id { get; private set; }
        public int Number { get; private set; } 
        public InvoiceStatus Status { get; private set; }
        public List<InvoiceItem> Items { get; private set; } = new();
        public DateTime CreatedAt { get; private set; }

        private Invoice() { }

        public Invoice(int number, List<InvoiceItem> items)
        {
            if (items == null || !items.Any())
                throw new ArgumentException("A nota fiscal deve ter pelo menos um produto.");

            Number = number;
            Status = InvoiceStatus.Aberta; 
            Items = items;
            CreatedAt = DateTime.UtcNow;
        }

        public void MarkAsClosed()
        {
            if (Status != InvoiceStatus.Aberta)
                throw new InvalidOperationException("Não é possível fechar uma nota que não esteja aberta.");

            Status = InvoiceStatus.Fechada;
        }

        public void UpdateItems(List<InvoiceItem> newItems)
        {
            if (Status != InvoiceStatus.Aberta)
                throw new InvalidOperationException("Não é possível editar uma nota que já está fechada.");

            if (newItems == null || !newItems.Any())
                throw new ArgumentException("A nota fiscal deve ter pelo menos um produto.");

            Items.Clear();
            Items.AddRange(newItems);
        }
    }
}