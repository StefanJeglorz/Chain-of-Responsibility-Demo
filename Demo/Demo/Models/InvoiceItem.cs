namespace Demo.Models;

public class InvoiceItem : BaseEntity
{
    public int InvoiceId { get; set; }
    public virtual Invoice Invoice { get; set; }
    
    public string Artikel { get; set; }
    public decimal Price { get; set; }
}