using Accounting_file_archiving_system.Models;

namespace Accounting_file_achiving_system_tests
{
    [TestFixture]
    internal class InitializeInvoice
    {
        [Test]
        public void Add_One_Invoice()
        {
            var service = new InvoiceSystem();
            // Arrange
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
            // Assert
            Assert.AreEqual(1, service.invoices.Count());
        }

        [Test]
        public void Test_Constructor_InitializeInvoiceStatuses()
        {
            // Arrange
            var service = new InvoiceSystem();

            // Act
            var invoiceStatuses = service.invoiceStatuses;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(invoiceStatuses);
                Assert.AreEqual(2, invoiceStatuses.Count); // We expect the list to have two elements after initialization
                Assert.AreEqual(1, invoiceStatuses[0].ID);
                Assert.AreEqual("New", invoiceStatuses[0].StatusName);
                Assert.AreEqual(2, invoiceStatuses[1].ID);
                Assert.AreEqual("Reviewed", invoiceStatuses[1].StatusName);
            });
        }
    }
}
