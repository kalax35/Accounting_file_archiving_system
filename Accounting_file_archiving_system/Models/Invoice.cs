using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting_file_archiving_system.Models
{
    public class Invoice
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public int SupplierID { get; set; }
        public int StatusID { get; set; }
        public int DivisionID { get; set; }
    }

}
