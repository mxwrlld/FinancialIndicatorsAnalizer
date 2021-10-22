using FIAModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleFIA.Mocks
{
    class MocksFabric
    {

        public static List<FinancialResult> MockFinancialResults1 => new List<FinancialResult>()
        {
            new FinancialResult("2000", 1, 2300, 400),
            new FinancialResult("2000", 2, 2300, 2400),
            new FinancialResult("2000", 3, 300, 4000)
            //new FinancialResult("2000", 4, 100, 40000)
        };
        
        public static List<FinancialResult> MockFinancialResults2 => new List<FinancialResult>()
        {
            new FinancialResult("2020", 3, 10, 10),
            new FinancialResult("2020", 4, 30000, 4000)
        };

        public static List<FinancialResult> MockFinancialResults3 => new List<FinancialResult>()
        {
            new FinancialResult("2010", 1, 100000000, 50000),
            new FinancialResult("2010", 2, 220000000000, 300004000)
        };

        public static Dictionary<string, Enterprise> GetMockEnterprisesReports()
        {
            Dictionary<string, Enterprise> MockEnterprises = new Dictionary<string, Enterprise>
            {
                ["123"] = new Enterprise("ООО Сок", "123", "Московское шоссе"),
                ["456"] = new Enterprise("OOO Совсем Добрый", "456", "ул. Лукачева"),
                ["789"] = new Enterprise("ООО Безусловный", "789", "шоссе Яблочное")
            };


            for (int i = 0; i < MockFinancialResults1.Count; i++)
            {
                MockEnterprises["123"].AddFinancialResult(MockFinancialResults1[i]);
            }

            for (int i = 0; i < MockFinancialResults2.Count; i++)
            {
                MockEnterprises["456"].AddFinancialResult(MockFinancialResults2[i]);
            }

            for (int i = 0; i < MockFinancialResults3.Count; i++)
            {
                MockEnterprises["789"].AddFinancialResult(MockFinancialResults3[i]);
            }

            return MockEnterprises;


        }
    }
}
