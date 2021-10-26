using System;
using System.Collections.Generic;
using System.Text;

namespace FIAModel
{
    public class Enterprise
    {
        public string Name { get; }
        public string TIN { get; }
        public string LegalAddress { get; }

        public Dictionary<Tuple<int, int>, FinancialResult> FinancialResults { get; private set; }
            = new Dictionary<Tuple<int, int>, FinancialResult>();

        public Enterprise(string name, string tin, string legalAddress)
        {
            Name = name;
            TIN = tin;
            LegalAddress = legalAddress;
        }

        public void AddFinancialResult(FinancialResult financialResult)
        {
            FinancialResults.Add(new Tuple<int, int>(financialResult.Year, financialResult.Quarter) , financialResult);
        }

        public bool RemoveFinancialResult(FinancialResult financialResult)
        {
            var key = new Tuple<int, int>(financialResult.Year, financialResult.Quarter);
            return FinancialResults.Remove(key);
        }

        public bool RemoveFinancialResult(int year, int quarter)
        {
            var key = new Tuple<int, int>(year, quarter);
            return FinancialResults.Remove(key);
        }

        public override string ToString()
        {
            return String.Format("\nПредприятие" +
                $"\nНазвание: {Name}" +
                $"\nИНН: {TIN}" +
                $"\nАдрес: {LegalAddress}" +
                "\n----------------------------------");
        }
    }
}
