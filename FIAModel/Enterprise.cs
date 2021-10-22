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

        public List<FinancialResult> FinancialResults { get; } = new List<FinancialResult>();

        public Enterprise(string name, string tin, string legalAddress)
        {
            Name = name;
            TIN = tin;
            LegalAddress = legalAddress;
        }

        public void AddFinancialResult(FinancialResult financialResult)
        {
            /* Исправить: 
             * возможно добавление ФинРез. с одинаковыми
             * полями года и квартала. 
             */
            FinancialResults.Add(financialResult);
        }

        public bool RemoveFinancialResult(FinancialResult financialResult)
        {
            return FinancialResults.Remove(financialResult);
        }

        public bool RemoveFinancialResult(string year, int quarter)
        {
            var foundFinRes = FinancialResults.Find(fr => fr.Year == year && fr.Quarter == quarter);
            if(foundFinRes != null)
            {
                return FinancialResults.Remove(foundFinRes);
            }
            return false;
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
