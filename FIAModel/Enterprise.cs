using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FIAModel
{
    public class Enterprise
    {
        public string Name { get; set; }
        public string TIN { get; set; }
        public string LegalAddress { get; set; }

        public Dictionary<Tuple<Year, int>, FinancialResult> FinancialResults { get; set; }
            = new Dictionary<Tuple<Year, int>, FinancialResult>();


        public Enterprise(string name, string tin, string legalAddress)
        {
            Name = name;
            TIN = tin;
            LegalAddress = legalAddress;
        }

        public Enterprise(string name, string tin, string legalAddress, List<FinancialResult> FR): this(name, tin, legalAddress)
        {
            foreach (var fr in FR)
            {
                FinancialResults.Add(new Tuple<Year, int>(new Year(fr.Year.year), fr.Quarter), fr);
            }
        }

        public void AddFinancialResult(FinancialResult financialResult)
        {
            FinancialResults.Add(new Tuple<Year, int>(new Year(financialResult.Year.year), financialResult.Quarter) , financialResult);
        }

        public bool RemoveFinancialResult(FinancialResult financialResult)
        {
            var key = new Tuple<Year, int>(new Year(financialResult.Year.year), financialResult.Quarter);
            return FinancialResults.Remove(key);
        }

        public bool RemoveFinancialResult(int yearInt, int quarter)
        {
            var year = new Year(yearInt);
            var key = new Tuple<Year, int>(year, quarter);
            return FinancialResults.Remove(key);
        }

        public Enterprise FindFinancialResults(Tuple<Year, int> tuple)
        {
            Enterprise enterprise = new Enterprise(Name, TIN, LegalAddress);
            Year year = tuple.Item1;
            int quarter = tuple.Item2;
            if(year.year == -1)
            {
                var frList = this.FinancialResults.Where(fr => fr.Value.Quarter == quarter).Select(x => x.Value).ToList();
                foreach (var fr in frList)
                {
                    enterprise.AddFinancialResult(fr);
                }
            }
            if (quarter == -1)
            {
                var frList = this.FinancialResults.Where(fr => fr.Value.Year.year == year.year).Select(x => x.Value).ToList();
                foreach (var fr in frList)
                {
                    enterprise.AddFinancialResult(fr);
                }
            }
            return enterprise;
        }

        // Для верного удаления объектов в регистре
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (obj is Enterprise)
            {
                var enterprise = obj as Enterprise;
                if (Name == enterprise.Name
                    && TIN == enterprise.TIN)
                    return true;
            }
            return false;
        }

        public override string ToString()
        {
            StringBuilder enterprise = new StringBuilder();
            enterprise.Append($"\nНазвание: {Name}");
            enterprise.Append($"\n     ИНН: {TIN}");
            enterprise.Append($"\n   Адрес: {LegalAddress}");
            enterprise.Append("\n-------------------------------");
            return enterprise.ToString();
        }

        public decimal SumOfIncome
        {
            get => SumOf(fr => fr.Income);
        }

        public decimal SumOfConsumption
        {
            get => SumOf(fr => fr.Consumption);
        }

        public decimal SumOfProfit
        {
            get => SumOf(fr => fr.Profit);
        }

        public double SumOfRentability
        {
            get
            {
                double sum = 0;
                foreach (var fr in FinancialResults)
                {
                    sum += fr.Value.Rentability;
                }
                return sum;
            }
        }

        private decimal SumOf(Func<FinancialResult, decimal> getParameter)
        {
            decimal sum = 0;
            foreach (var fr in FinancialResults)
            {
                sum += getParameter(fr.Value);
            }
            return sum;
        }
    }
}
