using System;
using System.Collections.Generic;

#nullable disable

namespace FIADbContext.ModelDb
{
    public partial class FinancialResult
    {
        public int Year { get; set; }
        public int Quarter { get; set; }
        public decimal Income { get; set; }
        public decimal Consumption { get; set; }
        public string Enterprise { get; set; }

        public virtual Enterprise EnterpriseNavigation { get; set; }
    }
}
