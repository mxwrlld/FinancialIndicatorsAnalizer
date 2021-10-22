using FIAModel;
using System;
using System.Collections.Generic;
using System.Text;

/*
 * Класс вводится из соображений сохранения независимости класса Table
 */


namespace ConsoleFIA.UserInterface
{
    class RegistryOfEnterprisesTable : Table
    {
        private RegistryOfEnterprises Registry;
        public RegistryOfEnterprisesTable(List<TableColumn> columns, RegistryOfEnterprises registry) : base(columns)
        {
            Registry = registry;
        }

        public override void Print()
        {
            base.PrintHeader();

            var values = Registry.Enterprises.Values;
            Enterprise[] enterprises = new Enterprise[values.Count];
            values.CopyTo(enterprises, 0);

            for (int i = 0; i < enterprises.Length; i++)
            {
                Console.WriteLine();
                base.PrintContent(enterprises[i].Name, Columns[0], isContentPositionFirst: true);
                for (int j = 0; j < enterprises[i].FinancialResults.Count; ++j)
                {
                    if(j > 0)
                    {
                        Console.WriteLine();
                        base.PrintContent(null, Columns[0], isContentPositionFirst: true);
                    }

                    var financialResult = enterprises[i].FinancialResults[j];
                    base.PrintContent(financialResult.Year, Columns[1]);
                    base.PrintContent(financialResult.Quarter.ToString(), Columns[2]);

                    string profit = String.Format($"{financialResult.Profit:C2}");
                    base.PrintContent(profit, Columns[3]);

                    string rentability = String.Format($"{financialResult.Rentability:P2}");
                    base.PrintContent(rentability, Columns[4]);
                    
                }
            }

            base.PrintFooter();
        }
    }
}
