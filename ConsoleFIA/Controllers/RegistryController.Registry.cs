using FIAModel;
using ConsoleFIA.UserInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ConsoleFIA.Controllers
{
    partial class RegistryController
    {
        private bool isRegistryNeedsInitialization = true;
        private bool isEnterpriseFinResNeedsInitialization = true;

        private Menu RegistryMenu;
        private Menu SortMenu;
        private Menu ModifyMenuForRegistry;
        private Menu ExternalCommunicationMenu;

        private Table<Enterprise> registryTable;
        private Table<FinancialResult> financialResultsTable;

        private SelectFromList<Enterprise> SelectEnterprises { get; set; }

        public Enterprise SelectedEnterprise
        {
            get
            {
                SelectEnterprises.Nodes = OrderedEnterprises;
                return SelectEnterprises.SelectedNode;
            }
        }

        private SelectFromList<FinancialResult> SelectFinancialResults { get; set; }

        public FinancialResult SelectedFinancialResult => SelectFinancialResults.SelectedNode;

        private List<Enterprise> OrderedEnterprises
        {
            get
            {
                var enterprises = Registry.Enterprises;
                if (!string.IsNullOrEmpty(SearchBy))
                {
                    enterprises = enterprises.Where(entr => entr.Name.ToLower().Contains(SearchBy) || entr.TIN.ToLower().Contains(SearchBy) || entr.LegalAddress.ToLower().Contains(SearchBy))
                    .ToList();
                }

                return enterprises.OrderBy(orderBy).ToList();
            }
        }

        private void RegistryFieldsInitialization()
        {
            RegistryMenu = new Menu(new List<MenuItem>()
            {
                new MenuAction(ConsoleKey.A, "Добавить предприятие", AddEnterprise),
                new MenuAction(ConsoleKey.S, "Сортировка", SortMenuSubPage),
                new MenuAction(ConsoleKey.D, "Поиск", Search)
            });
            ModifyMenuForRegistry = new Menu(new List<MenuItem>()
            {
                new MenuAction(ConsoleKey.Enter, "Детальный осмотр", () => { while(EnterprisePage()) ; })
            });
            ExternalCommunicationMenu = new Menu(new List<MenuItem>()
            {
                new MenuClose(ConsoleKey.Q, "Назад")
            });

            SortMenu = new Menu(new List<MenuItem>
            {
                new MenuAction(ConsoleKey.Z, "Сортировка по имени",
                    () => { orderBy = (enterprise => enterprise.Name); }),
                new MenuAction(ConsoleKey.X, "Сортировка по ИНН",
                    () => { orderBy = (enterprise => enterprise.TIN); }),
                new MenuAction(ConsoleKey.C, "Сортировка по адресу",
                    () => { orderBy = (enterprise => enterprise.LegalAddress); }),
                new MenuClose(ConsoleKey.Q, "Назад"),
            });

            int indentLength = 2;
            registryTable = new Table<Enterprise>(new List<TableColumn<Enterprise>>
            {
                new TableColumn<Enterprise>("Предприятие", indentLength,
                    Table<Enterprise>.GetMaxContentLength(Enterprises.Select(x => x.Name.ToString()).ToList()),
                    entr => entr.Name),
                new TableColumn<Enterprise>("ИНН", indentLength,
                    10, entr => entr.TIN),
                new TableColumn<Enterprise>("Адрес", indentLength,
                    Table<Enterprise>.GetMaxContentLength(Enterprises.Select(x => x.LegalAddress.ToString()).ToList()),
                    entr => entr.LegalAddress),
            });

            SearchBy = null;
            orderBy = (enterprise => enterprise.Name);
            SelectTotalFinancialResults.SelectedNodeIndex = 0;

            SelectEnterprises = new SelectFromList<Enterprise>(OrderedEnterprises);
        }

        private void UpdateWidthOfRegistryTable()
        {
            registryTable.Columns[0].MaxContentLength = Table<Enterprise>.GetMaxContentLength(Enterprises.Select(x => x.Name.ToString()).ToList());
            registryTable.Columns[2].MaxContentLength = Table<Enterprise>.GetMaxContentLength(Enterprises.Select(x => x.LegalAddress.ToString()).ToList());

        }

        private void EnterpriseFieldsInitialization(Enterprise enterprise)
        {
            List<FinancialResult> financialResults = enterprise.FinancialResults.Values.ToList();
            SelectFinancialResults = new SelectFromList<FinancialResult>(financialResults);
        }

        private bool EnterprisePage()
        {
            Console.Clear();

            Enterprise enterprise = SelectedEnterprise;
            List<FinancialResult> financialResults = enterprise.FinancialResults.Values.ToList();
            financialResultsTable = GetFinResTable(enterprise);
            if (isEnterpriseFinResNeedsInitialization)
            {
                EnterpriseFieldsInitialization(enterprise);
                isEnterpriseFinResNeedsInitialization = false;
            }

            financialResultsTable.SetWindowSize();

            ExternalCommunicationMenu.PrintSubMenu(ConsoleColor.DarkMagenta);
            SelectFinancialResults.Menu.PrintSubMenu();
            Console.WriteLine();
            Console.Write("Предприятие");

            Console.WriteLine(enterprise);

            Console.Write("Финансовые результаты");
            if (financialResults.Count == 0)
            {
                Console.Write("\n\tНи одной записи не найдено");
            }
            else
            {
                financialResultsTable.Print(financialResults, SelectedFinancialResult);
            }


            var key = Console.ReadKey().Key;

            SelectFinancialResults.Menu.UserChoice(key);
            bool userChoice = ExternalCommunicationMenu.UserChoice(key);
            if (!userChoice)
                isEnterpriseFinResNeedsInitialization = true;
            return userChoice;
        }

        private Table<FinancialResult> GetFinResTable(Enterprise enterprise)
        {
            int indentLength = 1;
            return new Table<FinancialResult>(new List<TableColumn<FinancialResult>>
            {
                 new TableColumn<FinancialResult>("Год", indentLength, 4, fr => fr.Year.ToString()),
                 new TableColumn<FinancialResult>("Квартал", indentLength, 7, fr => fr.Quarter.ToString()),
                 new TableColumn<FinancialResult>("Доход", indentLength,
                     Table<FinancialResult>.GetMaxContentLength(enterprise.FinancialResults.Select(fr => String.Format($"{fr.Value.Income:C2}")).ToList()),
                        fr => String.Format($"{fr.Income:C2}")),
                    new TableColumn<FinancialResult>("Расход", indentLength,
                        Table<Enterprise>.GetMaxContentLength(enterprise.FinancialResults.Select(fr =>      String.Format($"{fr.Value.Consumption:C2}")).ToList()),
                        fr => String.Format($"{fr.Consumption:C2}")),
                    new TableColumn<FinancialResult>("Прибыль", indentLength,
                        Table<Enterprise>.GetMaxContentLength(enterprise.FinancialResults.Select(fr => String.Format($"{fr.Value.Profit:C2}")).ToList()),
                        fr => String.Format($"{fr.Profit:C2}")),
                    new TableColumn<FinancialResult>("Рентабельность", indentLength,
                        Table<Enterprise>.GetMaxContentLength(enterprise.FinancialResults.Select(fr => String.Format($"{fr.Value.Rentability:P2}")).ToList()),
                        fr => String.Format($"{fr.Rentability:P2}"))
            });
        }

        public bool RegistryOfEnterprisesPage()
        {
            Console.Clear();
            if (isRegistryNeedsInitialization)
            {
                RegistryFieldsInitialization();
                isRegistryNeedsInitialization = false;
            }

            registryTable.SetWindowSize();

            RegistryMenu.PrintSubMenu();
            SelectEnterprises.Menu.PrintSubMenu();
            Console.WriteLine();
            ModifyMenuForRegistry.PrintSubMenu(ConsoleColor.DarkYellow);
            Console.WriteLine();
            ExternalCommunicationMenu.PrintSubMenu(ConsoleColor.DarkMagenta);

            registryTable.Print(OrderedEnterprises, SelectedEnterprise);

            var key = Console.ReadKey().Key;

            RegistryMenu.UserChoice(key);
            ModifyMenuForRegistry.UserChoice(key);
            SelectEnterprises.Menu.UserChoice(key);

            bool userChoice = ExternalCommunicationMenu.UserChoice(key);
            if (!userChoice)
                isRegistryNeedsInitialization = true;
            return userChoice;
        }

        private void SortMenuSubPage()
        {
            Console.Clear();
            registryTable.SetWindowSize();
            SortMenu.PrintSubMenu();

            registryTable.Print(OrderedEnterprises, SelectedEnterpriseForTFR);

            var key = Console.ReadKey().Key;
            SortMenu.UserChoice(key);
        }


        private void AddEnterprise()
        {
            Console.Clear();
            do
            {
                Enterprise enterprise;

                Console.WriteLine();
                Console.WriteLine("Получение информации о предприятии");
                Console.WriteLine();

                string name = InputValidator.ReadName();

                var existingEnterprise = Registry.FindEnterprise(name);
                if (existingEnterprise != null)
                {
                    Console.WriteLine("Предприятие уже существует!");
                    Console.WriteLine(existingEnterprise);
                    Console.WriteLine("Желаете изменить информацию?");
                    Console.WriteLine("Да - [любая кнопка] || Нет - [N]");
                    if (Console.ReadKey().Key != ConsoleKey.N)
                    {
                        enterprise = existingEnterprise;
                        string tin = InputValidator.ReadTIN();
                        string legalAddress = InputValidator.ReadAddress();

                        enterprise.TIN = tin;
                        enterprise.LegalAddress = legalAddress;

                        Console.WriteLine();
                        Console.WriteLine("Информация успешно обновлена");
                        break;
                    }
                    else
                        break;
                }
                else
                {
                    string tin = InputValidator.ReadTIN();
                    string legalAddress = InputValidator.ReadAddress();

                    enterprise = new Enterprise(name, tin, legalAddress);
                    try
                    {
                        Registry.AddEnterprise(enterprise);
                        Console.WriteLine(enterprise);
                        break;
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine("Ошибка: " + ex.Message);
                        Console.WriteLine("Повторите ввод");
                    }
                }
            } while (true);
            UpdateWidthOfRegistryTable();
        }
    }

}
