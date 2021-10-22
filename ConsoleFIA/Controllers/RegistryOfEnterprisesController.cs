using FIAModel;
using ConsoleFIA.Mocks;
using ConsoleFIA.UserInterface;
using System;
using System.Collections.Generic;

namespace ConsoleFIA.Controllers
{
    class RegistryOfEnterprisesController
    {
        public RegistryOfEnterprises Registry { get; }

        public RegistryOfEnterprisesController()
        {
            Registry = new RegistryOfEnterprises();
            Registry.Enterprises = MocksFabric.GetMockEnterprisesReports();
        }

        public void FillRecord()
        {
            Console.Clear();

            Enterprise enterprise;

            do
            {
                /*
                 * Возможность выбора имеющегося предприятия 
                 * без ввода инн и адреса 
                 */

                Console.WriteLine();
                Console.WriteLine("Получение информации о предприятии");
                Console.WriteLine();

                string name = InputValidator.ReadName();

                var existingEnterprise = Registry.FindEnterprise(name);
                if (existingEnterprise != null)
                {
                    Console.WriteLine("Искомое предприятие: ");
                    Console.WriteLine(existingEnterprise);
                    Console.WriteLine("Да - [любая кнопка] || Нет - [N]");
                    enterprise = existingEnterprise;
                }
                else
                {
                    string tin = InputValidator.ReadTIN();
                    string legalAddress = InputValidator.ReadAddress();

                    enterprise = new Enterprise(name, tin, legalAddress);
                    Registry.AddEnterprise(enterprise);
                    Console.WriteLine(enterprise);
                    break;
                }

            } while (Console.ReadKey().Key == ConsoleKey.N);


            do
            {
                Console.WriteLine();
                Console.WriteLine("Заполнение финансовой отчётности");

                string year = InputValidator.ReadYear();
                int quarter = InputValidator.ReadQuarter(year);
                decimal income = InputValidator.ReadIncome();
                decimal consumption = InputValidator.ReadConsumption();

                FinancialResult financialResult = new FinancialResult(year, quarter, income, consumption);
                enterprise.AddFinancialResult(financialResult);

                Console.WriteLine(financialResult);

                Console.WriteLine("Продолжить - любая кнопка  |" + "|  Выход - ESC");
            } while (Console.ReadKey().Key != ConsoleKey.Escape);
        }

        public void RegistryTableForm()
        {

            var maxContentLengthOfColumns = GetMaxContentLengthOfColumns();

            int indentLength = 2;
            RegistryOfEnterprisesTable table = new RegistryOfEnterprisesTable(new List<TableColumn>
            {
                new TableColumn("Предприятие", indentLength, maxContentLengthOfColumns[0]),
                new TableColumn("Год", indentLength, 4),
                new TableColumn("Квартал", indentLength, 1),
                new TableColumn("Прибыль", indentLength, maxContentLengthOfColumns[1]),
                new TableColumn("Рентабельность", indentLength, maxContentLengthOfColumns[2])
            }, Registry);


            int maxWindowWidth = 6; // Количество соединительных символов вроде "┌", "┐" и т.д.
            foreach (var column in table.Columns)
            {
                maxWindowWidth += column.Width;
            }
            Console.SetWindowSize(maxWindowWidth, 20);

            Console.Clear();
            table.Print();
            Console.ReadKey();
        }

        private int[] GetMaxContentLengthOfColumns()
        {
            var maxWidthOfColumns = new int[3];

            var values = Registry.Enterprises.Values;
            Enterprise[] enterprises = new Enterprise[values.Count];
            values.CopyTo(enterprises, 0);

            int maxLengthOfEnterpriseName = 0,
                maxLengthOfProfit = 0,
                maxLengthOfRentability = 0;
            foreach (var enterprise in enterprises)
            {
                if (maxLengthOfEnterpriseName < enterprise.Name.Length)
                    maxLengthOfEnterpriseName = enterprise.Name.Length;

                foreach (var finRes in enterprise.FinancialResults)
                {
                    string profit = String.Format($"{finRes.Profit:C2}");
                    if (maxLengthOfProfit < profit.Length)
                        maxLengthOfProfit = profit.Length;

                    string rentability = String.Format($"{finRes.Rentability:P2}");
                    if (maxLengthOfRentability < rentability.Length)
                        maxLengthOfRentability = rentability.Length;
                }
            }


            maxWidthOfColumns[0] = maxLengthOfEnterpriseName;
            maxWidthOfColumns[1] = maxLengthOfProfit;
            maxWidthOfColumns[2] = maxLengthOfRentability;
            return maxWidthOfColumns;
        }

    }
}
