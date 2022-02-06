using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FIADbContext.Model.DTO;

namespace FIAWebApi.BL.Model
{
    public class EnterpriseApiDTO
    {

        public string Name { get; set; }
        public string TIN { get; set; }
        public string LegalAddress { get; set; }

        public IEnumerable<FinancialResultApiDTO> FinancialResults { get; set; }

        public EnterpriseApiDTO() { }

        public EnterpriseApiDTO(EnterpriseDbDTO enterprise)
        {
            Name = enterprise.Name;
            TIN = enterprise.TIN;
            LegalAddress = enterprise.LegalAddress;
            FinancialResults = enterprise.FinancialResults.Select(finres => new FinancialResultApiDTO(finres));
        }

        public EnterpriseDbDTO Create()
        {
            return new EnterpriseDbDTO()
            {
                Name = Name,
                TIN = TIN,
                LegalAddress = LegalAddress,
                FinancialResults = FinancialResults?.Select(finres => finres.Create()).ToList()
            };
        }

        public void Update(EnterpriseDbDTO enterprise)
        {
            enterprise.TIN = TIN;
            enterprise.Name = Name;
            enterprise.LegalAddress = LegalAddress;

            foreach(var finres in FinancialResults)
            {
                var finresDbDTO = enterprise.FinancialResults.FirstOrDefault(fr => fr.Year == finres.Year && fr.Quarter == fr.Quarter);
                if (finresDbDTO == null)
                {
                    enterprise.FinancialResults.Add(finres.Create());
                }
                else
                {
                    finres.Update(finresDbDTO);
                }
            }
        }
    }
}
