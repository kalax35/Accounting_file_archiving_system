using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting_file_archiving_system.Models
{
    public class User
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Role Role { get; set; }
        public int DepartmentID { get; set; }
        public int DivisionID { get; set; }
    }

    public enum Role
    {
        TechnicalAdministrator,
        BusinessAdministrator,
        ReviewingEditingUser,
        ReviewingUser
    }
}
