﻿using Accounting_file_archiving_system.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting_file_achiving_system_tests
{
    internal class RejectInvoic
    {
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