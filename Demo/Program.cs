using Demo.Builder;
using Demo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("Starting  \nRegistering services");
var services = IServiceProviderBuilder.BuildServiceProvider();
Console.WriteLine("Services registered");

WaitForCommandInput:
Console.Write("""

              1.Create invoice
              2.Add invoice position
              3.Create invoice number and document
              4.Get all invoices

              Enter command:
              """);
var commandText = Console.ReadLine();
if (int.TryParse(commandText, out int command))
{
    Console.WriteLine($"Command {commandText} selected");
    switch (command)
    {
        case 1:
            await CreateInvoiceAsync();
            break;
        case 2:
            await AddInvoiceItemAsync();
            break;
        case 3:
            break;
        case 4:
            await GetInvoicesAsync();
            break;
    }

    goto WaitForCommandInput;
}
else
{
    Console.WriteLine("Invalid input");
    goto WaitForCommandInput;
}

async Task CreateInvoiceAsync()
{
    DemoDbContext context = services.GetRequiredService<DemoDbContext>();
    var invoice = new Invoice();
    await context.Invoices.AddAsync(invoice);
    await context.SaveChangesAsync();
    Console.WriteLine($"Added invoice with Id {invoice.Id}");
}

async Task AddInvoiceItemAsync()
{
    DemoDbContext context = services.GetRequiredService<DemoDbContext>();

    int invoiceId;
    EnterInvoiceId:
    Console.WriteLine($"Enter invoice id");
    while (!int.TryParse(Console.ReadLine(), out invoiceId))
    {
        goto EnterInvoiceId;
    }

    var invoice = await context.Invoices.Include(x => x.InvoiceItems).FirstOrDefaultAsync(x => x.Id == invoiceId);
    if (invoice is null)
    {
        Console.WriteLine($"Invoice with Id {invoiceId} not found");
        return;
    }

    EnterArticle:
    Console.WriteLine($"Enter article name");
    var article = Console.ReadLine();
    while (string.IsNullOrWhiteSpace(article))
    {
        goto EnterArticle;
    }

    EnterPrice:
    decimal price;
    Console.WriteLine($"Enter price name");
    while (!decimal.TryParse(Console.ReadLine(), out price))
    {
        goto EnterPrice;
    }

    var invoiceItem = new InvoiceItem()
    {
        Artikel = article,
        Price = price,
    };

    invoice.InvoiceItems.Add(invoiceItem);
    await context.SaveChangesAsync();
    Console.WriteLine($"Invoice with Id {invoice.Id} contains {invoice.InvoiceItems.Count}");
}

async Task GetInvoicesAsync()
{
    DemoDbContext context = services.GetRequiredService<DemoDbContext>();
    var invoices = await context.Invoices.ToListAsync();
    Console.WriteLine("Invoices:");
    invoices.ForEach(invoice => Console.WriteLine(invoice));
}