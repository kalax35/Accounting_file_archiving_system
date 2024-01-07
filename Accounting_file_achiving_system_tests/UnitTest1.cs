using Accounting_file_archiving_system.Models;
using System.Reflection;

namespace Accounting_file_achiving_system_tests
{
    [TestFixture]
    public class PrivateMethods
    {

        [Test]
        public void Test_GetUserRole()
        {
            var service = new InvoiceSystem();

            // Arrange
            var privateMethod = typeof(InvoiceSystem).GetMethod("GetUserRole", BindingFlags.NonPublic | BindingFlags.Instance);
            var user = new User { ID = 1, FirstName = "John", LastName = "Doe", Role = Role.BusinessAdministrator };
            service.users.Add(user);

            // Act
            var result = (Role)privateMethod.Invoke(service, new object[] { user.ID });

            // Assert
            Assert.AreEqual(Role.BusinessAdministrator, result);
        }
        [Test]
        public void Test_GetUserRole_NonExistentUser()
        {
            var service = new InvoiceSystem();

            // Arrange
            var privateMethod = typeof(InvoiceSystem).GetMethod("GetUserRole", BindingFlags.NonPublic | BindingFlags.Instance);

            // Act
            var result = (Role)privateMethod.Invoke(service, new object[] { 1 }); // ID not exist

            // Assert
            Assert.AreEqual(Role.ReviewingUser, result);
        }

        [Test]
        public void Test_FindInvoiceAndUser()
        {
            var service = new InvoiceSystem();

            // Arrange
            var privateMethod = typeof(InvoiceSystem).GetMethod("FindInvoiceAndUser", BindingFlags.NonPublic | BindingFlags.Instance);
            var user = new User { ID = 1, FirstName = "John", LastName = "Doe", Role = Role.BusinessAdministrator };
            var invoice = new Invoice { ID = 1, Date = DateTime.UtcNow, Amount = 1000.00m, SupplierID = 1, DivisionID = 1 };
            service.users.Add(user);
            service.invoices.Add(invoice);

            // Act
            var result = ((Invoice, User))privateMethod.Invoke(service, new object[] { invoice.ID, user.ID });

            // Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(invoice, result.Item1);
                Assert.AreEqual(user, result.Item2);
            });
        }

        [Test]
        public void Test_FindInvoiceAndUser_NonExistentEntities()
        {
            var service = new InvoiceSystem();

            // Arrange
            var privateMethod = typeof(InvoiceSystem).GetMethod("FindInvoiceAndUser", BindingFlags.NonPublic | BindingFlags.Instance);

            // Act
            var result = ((Invoice, User))privateMethod.Invoke(service, new object[] { 1, 1 }); // An ID that does not exist

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsNull(result.Item1); // We expect that the invoice does not exist
            Assert.IsNull(result.Item2); // We expect that the user does not exist
            });
        }

    }
}