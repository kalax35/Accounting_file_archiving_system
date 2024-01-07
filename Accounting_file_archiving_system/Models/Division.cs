using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting_file_archiving_system.Models
{
    public class Division
    {
        public int ID { get; set; }
        public string DivisionName { get; set; }
        public int DepartmentID { get; set; }
    }
}
