using Accounting_file_archiving_system.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Accounting_file_achiving_system_tests
{
    [TestFixture]
    public class GeneralIntegrationTests
    {
        [Test]
        public void Test_AddEditReviewInvoiceWorkflow()
        {
            // Arrange
            var service = new InvoiceSystem();
            service.users.AddRange(new List<User>
            {
                new User { ID = 1, FirstName = "Alice", LastName = "Administrator", Role = Role.BusinessAdministrator },
                new User { ID = 2, FirstName = "Bob", LastName = "Reviewer", Role = Role.ReviewingEditingUser }
            });
            List<InvoiceStatus> invoiceStatuses = new List<InvoiceStatus>();
            var invoice = new Invoice
            {
                ID = 1,
                Date = DateTime.UtcNow,
                Amount = 1000.00m,
                SupplierID = 1,
                DivisionID = 1,
            };
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
            // Act
            service.AddInvoice(invoice);
            service.EditInvoice(invoice.ID, service.users[1].ID, "1200,00");
            service.ReviewInvoice(invoice.ID, service.users[1].ID);

            // Assert
            var statusName = service.invoiceStatuses.Where(x=>x.ID== service.invoices[0].StatusID).First().StatusName;
            Assert.AreEqual("Reviewed", statusName); // Expecting Reviewed status
        }
        [Test]
        public void AddInvoice_InvalidStatus()
        {
            var service = new InvoiceSystem();

            // Arrange
            var invalidStatusInvoice = new Invoice
            {
                ID = 1,
                Date = DateTime.UtcNow,
                Amount = 1000.00m,
                SupplierID = 1,
                DivisionID = 1,
                StatusID = 999,
            };

            // Act
            service.AddInvoice(invalidStatusInvoice);

            // Assert
            Assert.AreEqual(1, service.invoices[0].StatusID);
        }

        [Test]
        public void Test_ApproveArchiveInvoiceWorkflow()
        {
            // Arrange
            var service = new InvoiceSystem();
            service.users.AddRange(new List<User>
            {
                new User { ID = 1, FirstName = "Alice", LastName = "Administrator", Role = Role.BusinessAdministrator },
                new User { ID = 2, FirstName = "Bob", LastName = "Reviewer", Role = Role.ReviewingEditingUser }
            });
            List<InvoiceStatus> invoiceStatuses = new List<InvoiceStatus>();
            var invoice = new Invoice
            {
                ID = 1,
                Date = DateTime.UtcNow,
                Amount = 1000.00m,
                SupplierID = 1,
                DivisionID = 1,
            };
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

            // Act
            service.AddInvoice(invoice);
            service.ApproveInvoice(invoice.ID, service.users[0].ID);
            service.ArchiveInvoice(invoice.ID, service.users[0].ID);

            // Assert
            var statusName = service.invoiceStatuses.Where(x => x.ID == service.invoices[0].StatusID).First().StatusName;
            Assert.AreEqual("Archived", statusName); // Expecting Archived status
        }

        [Test]
        public void Test_RejectInvoiceWorkflow()
        {
            // Arrange
            var service = new InvoiceSystem();
            service.users.AddRange(new List<User>
            {
                new User { ID = 1, FirstName = "Alice", LastName = "Administrator", Role = Role.BusinessAdministrator },
                new User { ID = 2, FirstName = "Bob", LastName = "Reviewer", Role = Role.ReviewingEditingUser }
            });
            List<InvoiceStatus> invoiceStatuses = new List<InvoiceStatus>();
            var invoice = new Invoice
            {
                ID = 1,
                Date = DateTime.UtcNow,
                Amount = 1000.00m,
                SupplierID = 1,
                DivisionID = 1,
            };
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

            // Act
            service.AddInvoice(invoice);
            service.RejectInvoice(invoice.ID, service.users[1].ID);

            // Assert
            var statusName = service.invoiceStatuses.Where(x => x.ID == service.invoices[0].StatusID).First().StatusName;
            Assert.AreEqual("Rejected", statusName); // Expecting Rejected status
        }

        [Test]
        public void Test_GetUserRoleForExistingUser()
        {
            // Arrange
            var service = new InvoiceSystem();
            var user = new User { ID = 1, FirstName = "Existing", LastName = "User", Role = Role.ReviewingUser };

            // Act
            var userRole = InvokeGetUserRole(service, user.ID);

            // Assert
            Assert.AreEqual(Role.ReviewingUser, userRole);
        }

        [Test]
        public void Test_GetUserRoleForNonExistingUser()
        {
            // Arrange
            var service = new InvoiceSystem();

            // Act
            var userRole = InvokeGetUserRole(service, 999);

            // Assert
            Assert.AreEqual(Role.ReviewingUser, userRole); // Assuming default role for non-existing user
        }

        private Role InvokeGetUserRole(InvoiceSystem service, int userID)
        {
            // Reflection or other mechanisms to access private method
            var method = service.GetType().GetMethod("GetUserRole", BindingFlags.NonPublic | BindingFlags.Instance);
            return (Role)method.Invoke(service, new object[] { userID });
        }

    }

}
