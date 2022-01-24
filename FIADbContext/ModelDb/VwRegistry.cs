using System;
using System.Collections.Generic;

#nullable disable

namespace FIADbContext.ModelDb
{
    public partial class VwRegistry
    {
        public string Tin { get; set; }
        public string Name { get; set; }
        public string LegalAddress { get; set; }
        public int Year { get; set; }
        public int Quarter { get; set; }
        public decimal Income { get; set; }
        public decimal Consumption { get; set; }
    }
}
