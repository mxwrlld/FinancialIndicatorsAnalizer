using System;
using System.Collections.Generic;
using System.Text;
using FIADbContext.Model.DTO;
using FIAModel;
using System.Linq;

namespace ConsoleFIA.DB.Mapping
{
    static class EnterpriseMapper
    {
        public static Enterprise Map(EnterpriseDbDTO enterprise)
        {
            if (enterprise == null)
                return null;
            return new Enterprise(
                enterprise.Name,
                enterprise.TIN,
                enterprise.LegalAddress,
                enterprise.FinancialResults.Select(finres => FinancialResultMapper.Map(finres)).ToList()
                );
        }

        public static EnterpriseDbDTO Map(Enterprise enterprise)
        {
            if (enterprise == null)
                return null;
            return new EnterpriseDbDTO()
            {
                Name = enterprise.Name,
                TIN = enterprise.TIN,
                LegalAddress = enterprise.LegalAddress,
                FinancialResults = enterprise.FinancialResults.Values.Select(finres => FinancialResultMapper.Map(finres)).ToList()
            };
        }
    }
}
