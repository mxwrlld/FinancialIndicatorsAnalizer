using System;
using System.Collections.Generic;
using FIADbContext.Model.DTO;
using FIAModel;
using System.Linq;

namespace ConsoleFIA.DB.Mapping
{
    static class FinancialResultMapper
    {
        public static FinancialResult Map(FinancialResultDbDTO financialResult)
        {
            if (financialResult == null)
                return null;
            return new FinancialResult(
                financialResult.Year,
                financialResult.Quarter,
                financialResult.Income,
                financialResult.Consumption
                );
        }

        public static FinancialResultDbDTO Map(FinancialResult financialResult)
        {
            if (financialResult == null)
                return null;
            return new FinancialResultDbDTO()
            {
                Year = financialResult.Year.year,
                Quarter = financialResult.Quarter,
                Income = financialResult.Income,
                Consumption = financialResult.Consumption
            };
        }
    }
}
