using System;
using System.Text;

namespace FIAModel
{
    public class FinancialResult
    {
        public Year Year { get; set; }
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

        public FinancialResult(int year, int quarter, decimal income, decimal consumption)
        {
            Year = new Year(year);
            Quarter = quarter;
            Income = income;
            Consumption = consumption;
        }

        public override string ToString()
        {
            StringBuilder financialResult = new StringBuilder();
            financialResult.Append($"\nГод:            {Year}");
            financialResult.Append($"\nКвартал:        {Quarter}");
            financialResult.Append($"\nДоход:          {Income}");
            financialResult.Append($"\nРасход:         {Consumption}");
            financialResult.Append($"\nРентабельность: {Rentability:P2}");
            financialResult.Append("\n**********************************");
            return financialResult.ToString();
        }
    }
}
