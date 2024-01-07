using Accounting_file_archiving_system.Models;

namespace Accounting_file_achiving_system_tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void AddOne_Invoice()
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
        public void Edit_Invoice_positif_path_float_value()
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
            service.EditInvoice(successfulInvoice.ID, 2, "1200,00");
            // Assert
            Assert.Multiple(() =>
            {
                Assert.AreNotEqual(1000.00m, service.invoices[0].Amount);
                Assert.AreEqual(1200.00m, service.invoices[0].Amount);
            });
        }

        [Test]
        public void Edit_Invoice_positif_path_int_value()
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
            service.EditInvoice(successfulInvoice.ID, 2, "1212");
            // Assert
            Assert.Multiple(() =>
            {
                Assert.AreNotEqual(1000.00m, service.invoices[0].Amount);
                Assert.AreEqual(1212, service.invoices[0].Amount);
            });
        }

        [Test]
        public void Edit_Invoice_wrang_format_of_amount()
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
            service.EditInvoice(successfulInvoice.ID, 2, "1200.00");
            // Assert
            Assert.Multiple(() =>
            {
                Assert.AreNotEqual(1200.00m, service.invoices[0].Amount);
                Assert.AreEqual(1000.00m, service.invoices[0].Amount);
            });
        }

        [Test]
        public void Edit_Invoice_text_in_amount()
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
            service.EditInvoice(successfulInvoice.ID, 2, "1200m");
            // Assert
            Assert.Multiple(() =>
            {
                Assert.AreNotEqual(1200.00m, service.invoices[0].Amount);
                Assert.AreEqual(1000.00m, service.invoices[0].Amount);
            });
        }

        [Test]
        public void Edit_Invoice_wrang_user_role()
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
            service.EditInvoice(successfulInvoice.ID, 1, "1200,00");
            // Assert
            Assert.Multiple(() =>
            {
                Assert.AreNotEqual(1200.00m, service.invoices[0].Amount);
                Assert.AreEqual(1000.00m, service.invoices[0].Amount);
            });
        }

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
        [Test]
        public void Approve_Invoice()
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
            service.AddInvoice(successfulInvoice);
            service.ApproveInvoice(successfulInvoice.ID, 1);
            // Assert
            Assert.Multiple(() =>
            {
                Assert.AreNotEqual(1200.00m, service.invoices[0].Amount);
                Assert.AreEqual(1000.00m, service.invoices[0].Amount);
                Assert.AreEqual(4, service.invoices[0].StatusID);
                Assert.AreEqual(service.invoiceStatuses[5].ID, service.invoices[0].StatusID); //+2 states from the initial
                Assert.AreEqual("Approved", service.invoiceStatuses[5].StatusName);//+2 states from the initial
            });
        }
        [Test]
        public void Approve_Invoice_wrang_role()
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
            service.AddInvoice(successfulInvoice);
            service.ApproveInvoice(successfulInvoice.ID, 2);
            // Assert
            Assert.Multiple(() =>
            {
                Assert.AreNotEqual(1200.00m, service.invoices[0].Amount);
                Assert.AreEqual(1000.00m, service.invoices[0].Amount);
                Assert.AreEqual(1, service.invoices[0].StatusID);
                Assert.AreEqual(service.invoiceStatuses[2].ID, service.invoices[0].StatusID); 
                Assert.AreEqual("New", service.invoiceStatuses[2].StatusName);
            });
        }
        [Test]
        public void Archive_Invoic()
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
            service.AddInvoice(successfulInvoice);
            service.ArchiveInvoice(successfulInvoice.ID, 1);
            // Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(1000.00m, service.invoices[0].Amount);
                Assert.AreEqual(7, service.invoices[0].StatusID);
                Assert.AreEqual(service.invoiceStatuses[8].ID, service.invoices[0].StatusID);
                Assert.AreEqual("Archived", service.invoiceStatuses[8].StatusName);
            });
        }
        [Test]
        public void Archive_Invoice_wrang_role()
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
            service.AddInvoice(successfulInvoice);
            service.ArchiveInvoice(successfulInvoice.ID, 2);
            // Assert
            Assert.Multiple(() =>
            {
                Assert.AreNotEqual(1200.00m, service.invoices[0].Amount);
                Assert.AreEqual(1000.00m, service.invoices[0].Amount);
                Assert.AreEqual(1, service.invoices[0].StatusID);
                Assert.AreEqual(service.invoiceStatuses[0].ID, service.invoices[0].StatusID);
                Assert.AreEqual("New", service.invoiceStatuses[0].StatusName);
            });
        }
        [Test]
        public void Reject_Invoic()
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
            service.AddInvoice(successfulInvoice); 
            service.RejectInvoice(successfulInvoice.ID, 2);
            // Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(1000.00m, service.invoices[0].Amount);
                Assert.AreEqual(8, service.invoices[0].StatusID);
                Assert.AreEqual(service.invoiceStatuses[9].ID, service.invoices[0].StatusID);
                Assert.AreEqual("Rejected", service.invoiceStatuses[9].StatusName);
            });
        }
        [Test]
        public void Reject_Invoice_wrang_role()
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
            service.AddInvoice(successfulInvoice);
            service.RejectInvoice(successfulInvoice.ID, 1);
            // Assert
            Assert.Multiple(() =>
            {
                Assert.AreNotEqual(1200.00m, service.invoices[0].Amount);
                Assert.AreEqual(1000.00m, service.invoices[0].Amount);
                Assert.AreEqual(1, service.invoices[0].StatusID);
                Assert.AreEqual(service.invoiceStatuses[0].ID, service.invoices[0].StatusID);
                Assert.AreEqual("New", service.invoiceStatuses[0].StatusName);
            });
        }
    }
}