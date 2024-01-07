﻿using Accounting_file_archiving_system.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting_file_achiving_system_tests
{
    [TestFixture]
    internal class EditInvoice
    {
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

    }
}