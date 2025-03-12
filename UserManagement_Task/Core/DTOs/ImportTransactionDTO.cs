using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Core.DTOs
{
    public class ImportTransactionDTO
    {
        public int TransactionID { get; set; }
        public Guid BatchID { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int? TotalRows { get; set; }
        public int? ProcessedRows { get; set; }
        public int? FailedRows { get; set; }
        public string Outcome { get; set; }
        public DateTime? LoggedAt { get; set; }

    }

}
