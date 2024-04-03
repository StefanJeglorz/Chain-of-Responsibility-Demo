namespace Demo.Models;

public class Invoice : BaseEntity
{
    public int? Number { get; set; }

    public virtual ICollection<InvoiceItem> InvoiceItems { get; set; }

    public override string ToString()
    {
        if (Number.HasValue)
        {
            return $"Invoice {Id} | Number {Number.Value}"; 
        }
        else
        {
            return $"Invoice {Id}";
        }
    }
}