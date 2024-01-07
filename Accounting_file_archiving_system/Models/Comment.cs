using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting_file_archiving_system.Models
{
    public class Comment
    {
        public int ID { get; set; }
        public int InvoiceID { get; set; }
        public int UserID { get; set; }
        public string Text { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
