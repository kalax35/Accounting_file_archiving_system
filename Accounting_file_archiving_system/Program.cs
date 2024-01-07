using Accounting_file_archiving_system.Models;
using Accounting_file_archiving_system;

var service = new InvoiceSystem();

service.users.AddRange(new List<User>
{
    new User { ID = 1, FirstName = "Alice", LastName = "Administrator", Role = Role.BusinessAdministrator },
    new User { ID = 2, FirstName = "Bob", LastName = "Reviewer", Role = Role.ReviewingEditingUser }
});

service.invoiceStatuses.AddRange(new List<InvoiceStatus>
{
    new InvoiceStatus { ID = 1, StatusName = InvoiceStatus.STATUS_NEW },
    new InvoiceStatus { ID = 2, StatusName = InvoiceStatus.STATUS_EDITED },
    new InvoiceStatus { ID = 3, StatusName = InvoiceStatus.STATUS_REVIEWED },
    new InvoiceStatus { ID = 4, StatusName = InvoiceStatus.STATUS_APPROVED },
    new InvoiceStatus { ID = 5, StatusName = InvoiceStatus.STATUS_SENT_TO_ACCOUNTING },
    new InvoiceStatus { ID = 6, StatusName = InvoiceStatus.STATUS_EDITED_BY_ACCOUNTING },
    new InvoiceStatus { ID = 7, StatusName = InvoiceStatus.STATUS_ARCHIVED },
    new InvoiceStatus { ID = 8, StatusName = InvoiceStatus.STATUS_REJECTED }
});

Console.WriteLine("Sukcesywny przebieg faktury:");
var successfulInvoice = new Invoice
{
    ID = 1,
    Date = DateTime.UtcNow,
    Amount = 1000.00m,
    SupplierID = 1,
    DivisionID = 1,
};

service.AddInvoice(successfulInvoice);
service.EditInvoice(successfulInvoice.ID, 2, "1200,00"); 
service.ReviewInvoice(successfulInvoice.ID, 2); 
service.ApproveInvoice(successfulInvoice.ID, 1); 
service.ArchiveInvoice(successfulInvoice.ID, 1); 

Console.WriteLine("Przebieg z odrzuceniem faktury:");
var rejectedInvoice = new Invoice
{
    ID = 2,
    Date = DateTime.UtcNow,
    Amount = 2000.00m,
    SupplierID = 1,
    DivisionID = 1,
};

service.AddInvoice(rejectedInvoice);
service.EditInvoice(rejectedInvoice.ID, 2, "2200,20"); 
service.ReviewInvoice(rejectedInvoice.ID, 2); 

service.RejectInvoice(rejectedInvoice.ID, 2);
