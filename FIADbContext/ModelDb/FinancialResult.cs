using System;
using System.Collections.Generic;

#nullable disable

namespace FIADbContext.ModelDb
{
    public sealed class FinancialResult
    {
        public int Year { get; set; }
        public int Quarter { get; set; }
        public decimal Income { get; set; }
        public decimal Consumption { get; set; }
        public string Enterprise { get; set; }

        public Enterprise EnterpriseNavigation { get; set; }
    }
}
