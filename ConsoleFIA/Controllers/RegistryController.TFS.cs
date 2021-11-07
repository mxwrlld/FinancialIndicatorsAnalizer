using System;
using System.Collections.Generic;
using System.Text;
using FIAModel;
using ConsoleFIA.Mocks;
using ConsoleFIA.UserInterface;
using System.Linq;

namespace ConsoleFIA.Controllers
{
    partial class RegistryController
    {
        private bool isTotalFSNeedsInitialization = true;
        private Menu TotalFinancialStatementsMenu;
        private Menu SortBySumMenu;

        private Table<Enterprise> totalFinancialStatementsTable;

        private void TotalFinancialStatementsFieldsInitialization()
        {
            TotalFinancialStatementsMenu = new Menu(new List<MenuItem>()
            {
                new MenuAction(ConsoleKey.S, "Сортировка", SortBySumMenuSubPage),
                new MenuAction(ConsoleKey.F, "Поиск", Search),
                new MenuClose(ConsoleKey.Q, "К фин. отчетности")
            });

            SortBySumMenu = new Menu(new List<MenuItem>()
            {
                new MenuAction(ConsoleKey.Z, "По сумм. дохода", SortBySumOfIncome),
                new MenuAction(ConsoleKey.X, "По сумм. расхода",SortBySumOfConsumption),
                new MenuAction(ConsoleKey.C, "По сумм. прибыли",SortBySumOfProfit),
                new MenuAction(ConsoleKey.V, "По сумм. рентабельности",SortBySumOfRentability),
                new MenuAction(ConsoleKey.B, "Отключить сортировку", DisableSorting),
                new MenuClose(ConsoleKey.Q, "Назад"),
            });

            int indentLength = 1;
            totalFinancialStatementsTable = new Table<Enterprise>(
                new List<TableColumn<Enterprise>>()
                {
                    new TableColumn<Enterprise>("Предприятие", indentLength,
                        Table<Enterprise>.GetMaxContentLength(Registry.Enterprises.Select(entr => entr.Name).ToList()),
                        entr => entr.Name),
                    new TableColumn<Enterprise>("Cум. Доход", indentLength,
                        Table<Enterprise>.GetMaxContentLength(Registry.Enterprises.Select(entr => String.Format($"{entr.SumOfIncome:C2}")).ToList()),
                        entr => String.Format($"{entr.SumOfIncome:C2}")),
                    new TableColumn<Enterprise>("Cум. Расход", indentLength,
                        Table<Enterprise>.GetMaxContentLength(Registry.Enterprises.Select(entr => String.Format($"{entr.SumOfConsumption:C2}")).ToList()), 
                        entr => String.Format($"{entr.SumOfConsumption:C2}")),
                    new TableColumn<Enterprise>("Cум. Прибыли", indentLength,
                        Table<Enterprise>.GetMaxContentLength(Registry.Enterprises.Select(entr => String.Format($"{entr.SumOfProfit:C2}")).ToList()), 
                        entr => String.Format($"{entr.SumOfProfit:C2}")),
                    new TableColumn<Enterprise>("Cум. Рентабельности", indentLength,
                        Table<Enterprise>.GetMaxContentLength(Registry.Enterprises.Select(entr => String.Format($"{entr.SumOfRentability:P2}")).ToList()), 
                        entr => String.Format($"{entr.SumOfRentability:P2}"))
                }
            );
        }

        public bool TotalFinancialStatementsPage()
        {
            if (isTotalFSNeedsInitialization)
            {
                TotalFinancialStatementsFieldsInitialization();
                isTotalFSNeedsInitialization = false;
            }
            Console.Clear();
            
            totalFinancialStatementsTable.SetWindowSize();
            TotalFinancialStatementsMenu.PrintSubMenu();

            totalFinancialStatementsTable.Print(OrderedEnterprises, SelectedEnterpriseForTFR);

            var key = Console.ReadKey().Key;
            bool userChoice = TotalFinancialStatementsMenu.UserChoice(key);
            if (!userChoice)
                isTotalFSNeedsInitialization = true;
            return userChoice;
        }

        private void SortBySumMenuSubPage()
        {
            Console.Clear();
            totalFinancialStatementsTable.SetWindowSize();

            SortBySumMenu.PrintSubMenu();

            totalFinancialStatementsTable.Print(OrderedFinancialStatements, SelectedEnterpriseForTFR);

            var key = Console.ReadKey().Key;
            SortBySumMenu.UserChoice(key);
        }
    }
}
