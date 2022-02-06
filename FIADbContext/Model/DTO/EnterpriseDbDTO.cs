using System;
using System.Collections.Generic;
using System.Text;

namespace FIADbContext.Model.DTO
{
    public class EnterpriseDbDTO
    {
        public string TIN { get; set; }
        public string Name { get; set; }
        public string LegalAddress { get; set; }
        public UserDbDTO Manager { get; set; }
        public ICollection<FinancialResultDbDTO> FinancialResults { get; set; }
    }
}
