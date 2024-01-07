using Accounting_file_archiving_system.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting_file_achiving_system_tests
{
    internal class ReviewInvoice
    {
        [Test]
        public void Review_Invoice()
        {
            var service = new InvoiceSystem();

            // Arrange
            service.users.AddRange(new List<User>
            {
                new User { ID = 1, FirstName = "Alice", LastName = "Administrator", Role = Role.BusinessAdministrator },
                new User { ID = 2, FirstName = "Bob", LastName = "Reviewer", Role = Role.ReviewingEditingUser }
            });
            List<InvoiceStatus> invoiceStatuses = new List<InvoiceStatus>();
            var successfulInvoice = new Invoice
            {
                ID = 1,
                Date = DateTime.UtcNow,
                Amount = 1000.00m,
                SupplierID = 1,
                DivisionID = 1,
            };

            // Act
            service.AddInvoice(successfulInvoice);
            service.ReviewInvoice(successfulInvoice.ID, 2);
            // Assert
            Assert.Multiple(() =>
            {
                Assert.AreNotEqual(1200.00m, service.invoices[0].Amount);
                Assert.AreEqual(1000.00m, service.invoices[0].Amount);
                Assert.AreEqual(2, service.invoices[0].StatusID);
                Assert.AreEqual(service.invoiceStatuses[1].ID, service.invoices[0].StatusID);
                Assert.AreEqual("Reviewed", service.invoiceStatuses[1].StatusName);
            });
        }
        [Test]
        public void Review_Invoice_wrang_role()
        {
            var service = new InvoiceSystem();

            // Arrange
            service.users.AddRange(new List<User>
            {
                new User { ID = 1, FirstName = "Alice", LastName = "Administrator", Role = Role.BusinessAdministrator },
                new User { ID = 2, FirstName = "Bob", LastName = "Reviewer", Role = Role.ReviewingEditingUser }
            });
            List<InvoiceStatus> invoiceStatuses = new List<InvoiceStatus>();
            var successfulInvoice = new Invoice
            {
                ID = 1,
                Date = DateTime.UtcNow,
                Amount = 1000.00m,
                SupplierID = 1,
                DivisionID = 1,
            };
            // Act
            service.AddInvoice(successfulInvoice);
            service.ReviewInvoice(successfulInvoice.ID, 1);
            // Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(1000.00m, service.invoices[0].Amount);
                Assert.AreEqual(1, service.invoices[0].StatusID);
                Assert.AreNotEqual(service.invoiceStatuses[1].ID, service.invoices[0].StatusID);
                Assert.AreNotEqual("Review", service.invoiceStatuses[0].StatusName);
                Assert.AreEqual("New", service.invoiceStatuses[0].StatusName);
            });
        }
    }
}
