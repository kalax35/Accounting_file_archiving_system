using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting_file_archiving_system.Models
{
    public class InvoiceStatus
    {
        public const string STATUS_NEW = "New";
        public const string STATUS_EDITED = "Edited";
        public const string STATUS_REVIEWED = "Reviewed";
        public const string STATUS_REJECTED = "Rejected";
        public const string STATUS_APPROVED = "Approved";
        public const string STATUS_SENT_TO_ACCOUNTING = "SentToAccounting";
        public const string STATUS_EDITED_BY_ACCOUNTING = "EditedByAccounting";
        public const string STATUS_ARCHIVED = "Archived";

        public int ID { get; set; }
        public string StatusName { get; set; }
    }
}
