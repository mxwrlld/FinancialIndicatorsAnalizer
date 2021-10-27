using FIAModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleFIA.Mocks
{
    class MocksFabric
    {

        public static Dictionary<Tuple<int, int>, FinancialResult> MockFinancialResults1 => new Dictionary<Tuple<int, int>, FinancialResult>()
        {
            [new Tuple<int, int>(2000, 1)] = new FinancialResult(2000, 1, 2300, 400),
            [new Tuple<int, int>(2000, 2)] = new FinancialResult(2000, 2, 2300, 2400),
            [new Tuple<int, int>(2000, 3)] = new FinancialResult(2000, 3, 300, 4000),
            //[new Tuple<int, int>(2000, 4)] = new FinancialResult(2000, 4, 100, 40000),
        };

        public static Dictionary<Tuple<int, int>, FinancialResult> MockFinancialResults2 => new Dictionary<Tuple<int, int>, FinancialResult>()
        {
            [new Tuple<int, int>(2020, 3)] = new FinancialResult(2020, 3, 10, 10),
            [new Tuple<int, int>(2020, 4)] = new FinancialResult(2020, 4, 30000, 4000)
        };

        public static Dictionary<Tuple<int, int>, FinancialResult> MockFinancialResults3 => new Dictionary<Tuple<int, int>, FinancialResult>()
        {
            [new Tuple<int, int>(2010, 1)] = new FinancialResult(2010, 1, 100000000, 50000),
            [new Tuple<int, int>(2010, 2)] = new FinancialResult(2010, 2, 220000000000, 300004000)
        };


        public static List<Enterprise> GetMockEnterprisesReports()
        {
            List<Enterprise> MockEnterprises = new List<Enterprise>()
            {
                new Enterprise("ООО Сок", "123", "Московское шоссе"),
                new Enterprise("OOO Совсем Добрый", "456", "ул. Лукачева"),
                new Enterprise("ООО Безусловный", "789", "шоссе Яблочное")
            };

            MockEnterprises[0].FinancialResults = MockFinancialResults1;
            MockEnterprises[1].FinancialResults = MockFinancialResults2;
            MockEnterprises[2].FinancialResults = MockFinancialResults3;
            
            return MockEnterprises;
        }
    }
}
