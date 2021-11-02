using FIAModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleFIA.Mocks
{
    class MocksFabric
    {

        public static Dictionary<Tuple<Year, int>, FinancialResult> MockFinancialResults1 => new Dictionary<Tuple<Year, int>, FinancialResult>()
        {
            [new Tuple<Year, int>(new Year(2000), 1)] = new FinancialResult(2000, 1, 2300, 400),
            [new Tuple<Year, int>(new Year(2000), 2)] = new FinancialResult(2000, 2, 2300, 2400),
            [new Tuple<Year, int>(new Year(2000), 3)] = new FinancialResult(2000, 3, 300, 4000),
            //[new Tuple<int, int>(2000, 4)] = new FinancialResult(2000, 4, 100, 40000),
        };

        public static Dictionary<Tuple<Year, int>, FinancialResult> MockFinancialResults2 => new Dictionary<Tuple<Year, int>, FinancialResult>()
        {
            [new Tuple<Year, int>(new Year(2020), 3)] = new FinancialResult(2020, 3, 10, 10),
            [new Tuple<Year, int>(new Year(2020), 4)] = new FinancialResult(2020, 4, 30000, 4000)
        };

        public static Dictionary<Tuple<Year, int>, FinancialResult> MockFinancialResults3 => new Dictionary<Tuple<Year, int>, FinancialResult>()
        {
            [new Tuple<Year, int>(new Year(2010), 1)] = new FinancialResult(2010, 1, 100000000, 50000),
            [new Tuple<Year, int>(new Year(2010), 2)] = new FinancialResult(2010, 2, 220000000000, 300004000)
        };


        public static List<Enterprise> GetMockEnterprisesReports()
        {
            List<Enterprise> MockEnterprises = new List<Enterprise>()
            {
                new Enterprise("ООО Сок", "222", "Московское шоссе"),
                new Enterprise("OOO Совсем Добрый", "105", "ул. Лукачева"),
                new Enterprise("ООО Безусловный", "056", "Яблочное шоссе"),
                new Enterprise("Ясеневый", "012", "ул. Ячменная"),
                new Enterprise("Альпийский округ", "013", "ул. Брянская"),
            };

            MockEnterprises[0].FinancialResults = MockFinancialResults1;
            MockEnterprises[1].FinancialResults = MockFinancialResults2;
            MockEnterprises[2].FinancialResults = MockFinancialResults3;
            
            return MockEnterprises;
        }
    }
}
