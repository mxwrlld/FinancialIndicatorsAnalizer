using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FIADbContext.Model.DTO;

namespace FIAWebApi.BL.Model
{
    public class FinancialResultApiDTO
    {
        public int Year { get; set; }
        public int Quarter { get; set; }
        public decimal Income { get; set; }
        public decimal Consumption { get; set; }
        public decimal Profit => (Income - Consumption);
        public double Rentability
        {
            get
            {
                if (Consumption == 0)
                    return (double)Profit;
                else
                    return (double)(Profit / Consumption);
            }
        }

        public FinancialResultApiDTO() {}

        public FinancialResultApiDTO(FinancialResultDbDTO financialResult) 
        {
            Year = financialResult.Year;
            Quarter = financialResult.Quarter;
            Income = financialResult.Income;
            Consumption = financialResult.Consumption;
        }

        public FinancialResultDbDTO Create()
        {
            return new FinancialResultDbDTO()
            {
                Year = Year,
                Quarter = Quarter,
                Income = Income,
                Consumption = Consumption,
            };
        }

        public void Update(FinancialResultDbDTO finres)
        {
            finres.Year = Year;
            finres.Quarter = Quarter;
            finres.Income = Income;
            finres.Consumption = Consumption;
        }

    }
}
