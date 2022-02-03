using System;
using System.Collections.Generic;
using System.Text;

namespace FIADbContext.Model.DTO
{
    class FinancialResultDbDTO
    {
        public int Year { get; set; }
        public int Quarter { get; set; }
        public decimal Income { get; set; }
        public decimal Consumption { get; set; }
        public EnterpriseDbDTO Enterprise { get; set; }
    }
}
