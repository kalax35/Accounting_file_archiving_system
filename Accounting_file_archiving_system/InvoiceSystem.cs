using Accounting_file_archiving_system.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting_file_archiving_system
{
    public class InvoiceSystem
    {
        public List<Invoice> invoices = new List<Invoice>();
        public List<User> users = new List<User>();
        public List<Supplier> suppliers = new List<Supplier>();
        public List<Comment> comments = new List<Comment>();
        public List<InvoiceStatus> invoiceStatuses = new List<InvoiceStatus>();

        public InvoiceSystem()
        {
            invoiceStatuses.AddRange(new List<InvoiceStatus>
            {
                new InvoiceStatus { ID = 1, StatusName = "New" },
                new InvoiceStatus { ID = 2, StatusName = "Reviewed" }
            });
        }

        public void AddInvoice(Invoice invoice)
        {
            try
            {
                var status = invoiceStatuses.FirstOrDefault(s => s.StatusName == InvoiceStatus.STATUS_NEW);

                if (status == null)
                {
                    Console.WriteLine("Error: 'New' status not found.");
                    return;
                }

                invoice.StatusID = status.ID;
                invoices.Add(invoice);
                Console.WriteLine($"Invoice with ID {invoice.ID} added successfully with 'New' status.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public void EditInvoice(int invoiceID, int userID, string newText)
        {
            try
            {
                var (invoice, user) = FindInvoiceAndUser(invoiceID, userID);

                if (invoice == null)
                {
                    Console.WriteLine($"Error: Invoice with ID {invoiceID} not found.");
                    return;
                }

                if (user == null || user.Role != Role.ReviewingEditingUser)
                {
                    Console.WriteLine($"Error: User with ID {userID} not found or does not have permission to edit.");
                    return;
                }

                if (!decimal.TryParse(newText, out var newAmount))
                {
                    Console.WriteLine("Error: Invalid amount format.");
                    return;
                }

                invoice.Amount = newAmount;
                var status = invoiceStatuses.SingleOrDefault(s => s.StatusName == InvoiceStatus.STATUS_EDITED);
                if (status == null)
                {
                    Console.WriteLine("Error: 'Edited' status not found.");
                    return;
                }

                invoice.StatusID = status.ID;
                Console.WriteLine($"Invoice with ID {invoice.ID} edited successfully.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public void ReviewInvoice(int invoiceID, int userID)
        {
            try
            {
                var (invoice, user) = FindInvoiceAndUser(invoiceID, userID);
                var status = invoiceStatuses.SingleOrDefault(s => s.StatusName == InvoiceStatus.STATUS_REVIEWED);

                if (invoice == null)
                {
                    Console.WriteLine($"Error: Invoice with ID {invoiceID} not found.");
                    return;
                }

                if (user == null || user.Role != Role.ReviewingEditingUser)
                {
                    Console.WriteLine($"Error: User with ID {userID} not found or does not have permission to review.");
                    return;
                }

                if (status == null)
                {
                    Console.WriteLine("Error: 'Reviewed' status not found.");
                    return;
                }

                invoice.StatusID = status.ID;
                Console.WriteLine($"Invoice with ID {invoice.ID} reviewed successfully.");

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public void ApproveInvoice(int invoiceID, int userID)
        {
            try
            {
                var invoice = invoices.SingleOrDefault(inv => inv.ID == invoiceID);
                var userRole = GetUserRole(userID);

                if (invoice == null)
                {
                    Console.WriteLine($"Error: Invoice with ID {invoiceID} not found.");
                    return;
                }

                if (userRole == null || userRole != Role.BusinessAdministrator)
                {
                    Console.WriteLine($"Error: User with ID {userID} not found or is not a Business Administrator.");
                    return;
                }

                var approvedStatus = invoiceStatuses.SingleOrDefault(status => status.StatusName == "Approved");
                if (approvedStatus == null)
                {
                    Console.WriteLine("Error: 'Approved' status not found.");
                    return;
                }

                invoice.StatusID = approvedStatus.ID;
                Console.WriteLine($"Invoice with ID {invoice.ID} approved successfully.");

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public void ArchiveInvoice(int invoiceID, int userID)
        {
            try
            {
                var invoice = invoices.SingleOrDefault(inv => inv.ID == invoiceID);
                var userRole = GetUserRole(userID);

                if (invoice == null)
                {
                    Console.WriteLine($"Error: Invoice with ID {invoiceID} not found.");
                    return;
                }

                if (userRole == null || userRole != Role.BusinessAdministrator)
                {
                    Console.WriteLine($"Error: User with ID {userID} not found or is not a Bussiness Administrator.");
                    return;
                }

                var archivedStatus = invoiceStatuses.SingleOrDefault(status => status.StatusName == InvoiceStatus.STATUS_ARCHIVED);
                if (archivedStatus == null)
                {
                    Console.WriteLine("Error: 'Archived' status not found.");
                    return;
                }

                invoice.StatusID = archivedStatus.ID;
                Console.WriteLine($"Invoice with ID {invoice.ID} archived successfully.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public void RejectInvoice(int invoiceID, int userID)
        {
            var invoice = invoices.SingleOrDefault(inv => inv.ID == invoiceID);
            var userRole = GetUserRole(userID);

            if (invoice == null)
            {
                Console.WriteLine($"Error: Invoice with ID {invoiceID} not found.");
                return;
            }

            if (userRole == null || userRole != Role.ReviewingEditingUser)
            {
                Console.WriteLine($"Error: User with ID {userID} not found or does not have permission to reject.");
                return;
            }

            var rejectedStatus = invoiceStatuses.SingleOrDefault(status => status.StatusName == InvoiceStatus.STATUS_REJECTED);
            if (rejectedStatus == null)
            {
                Console.WriteLine("Error: 'Rejected' status not found.");
                return;
            }

            invoice.StatusID = rejectedStatus.ID;
            Console.WriteLine($"Invoice with ID {invoice.ID} rejected successfully.");
        }


        private Role GetUserRole(int userID)
        {
            var user = users.FirstOrDefault(u => u.ID == userID);
            return user != null ? user.Role : Role.ReviewingUser;
        }

        private (Invoice invoice, User user) FindInvoiceAndUser(int invoiceID, int userID)
        {
            var invoice = invoices.Find(inv => inv.ID == invoiceID);
            var user = users.Find(u => u.ID == userID);

            return (invoice, user);
        }
    }
}
