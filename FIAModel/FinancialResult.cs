using System;

namespace FIAModel
{
    public class FinancialResult
    {
        public int Year { get; }
        public int Quarter { get; }
        public decimal Income { get;}
        public decimal Consumption { get;}
        public decimal Profit => (Income - Consumption);
        public double Rentability => (double)(Profit / Consumption);

        public FinancialResult(int year, int quarter, decimal income, decimal consumption)
        {
            Year = year;
            Quarter = quarter;
            Income = income;
            Consumption = consumption;
        }

        public override string ToString()
        {
            return String.Format($"\nГод: {Year}" +
                $"\nКвартал: {Quarter}" +
                $"\nДоход: {Income}" +
                $"\nРасход: {Consumption}" +
                $"\nПрибыль: {Profit}" +
                 "\nРентабельность: {0:P2}", Rentability +
                 "\n**********************************");
        }
    }
}
