using FIAModel;
using ConsoleFIA.Mocks;
using ConsoleFIA.UserInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ConsoleFIA.Files.DTO;

namespace ConsoleFIA.Controllers
{
    class FinancialStatementsController
    {
        public RegistryOfEnterprises Registry { get; }

        private string searchBy = null;
        private string SearchBy { get => searchBy; set => searchBy = value.ToLower(); }

        private Year FilterByYear = new Year(-1);
        private int FilterByQuarter = -1;
        private Func<Enterprise, object> orderBy = (enterprise => enterprise.Name);
        private List<Enterprise> OrderedEnterprises
        {
            get
            {
                var orderedEntr = (FilterByQuarter == -1 && FilterByYear.year == -1)
                ? Registry.Enterprises
                : ((FilterByQuarter == -1) ? Registry.FindEnterprises(FilterByYear)
                : Registry.FindEnterprises(FilterByQuarter));

                if (!string.IsNullOrEmpty(SearchBy))
                {
                    orderedEntr = orderedEntr.Where(entr => entr.Name.ToLower().Contains(SearchBy)).ToList();
                }

                return orderedEntr.OrderBy(orderBy).ToList();
            }
        }
            

        private List<FinancialResult> AllFinancialResults => Registry.GetAllFinancialResults();


        private readonly Menu FinancialStatementsMenu;
        private readonly Menu SortMenu;
        private readonly Menu FilterMenu;
        private Table<Enterprise, FinancialResult> table;

        public FinancialStatementsController()
        {
            Registry = new RegistryOfEnterprises();
            //Registry.Enterprises = MocksFabric.GetMockEnterprisesReports();
            FinancialStatementsMenu = new Menu(new List<MenuItem>()
            {
                new MenuAction(ConsoleKey.A, "Добавить запись", AddRecord),
                new MenuAction(ConsoleKey.S, "Сортировка", SortMenuSubPage),
                new MenuAction(ConsoleKey.D, "Фильтрация", FiltersMenuSubPage),
                new MenuAction(ConsoleKey.F, "Поиск", Search),
                new MenuAction(ConsoleKey.G, "Сохранить", SaveToFile),
                new MenuAction(ConsoleKey.H, "Загрузить", LoadFromFile),
                new MenuClose(ConsoleKey.Q, "Главное меню"),
            });
            SortMenu = new Menu(new List<MenuItem>()
            {
                new MenuAction(ConsoleKey.Z, "По сумм. дохода", SortBySumOfIncome),
                new MenuAction(ConsoleKey.X, "По сумм. расхода",SortBySumOfConsumption),
                new MenuAction(ConsoleKey.C, "По сумм. прибыли",SortBySumOfProfit),
                new MenuAction(ConsoleKey.V, "По сумм. рентабельности",SortBySumOfRentability),
                new MenuAction(ConsoleKey.B, "Отключить сортировку", DisableSorting),
                new MenuClose(ConsoleKey.Q, "Назад"),
            });
            FilterMenu = new Menu(new List<MenuItem>
            {
                new MenuAction(ConsoleKey.Z, "По году", FiltrationByYear),
                new MenuAction(ConsoleKey.X, "По кварталу", FiltrationByQuarter),
                new MenuAction(ConsoleKey.C, "Отключить фильтрацию", DisableFiltration),
                new MenuClose(ConsoleKey.Q, "Назад"),
            });

            CreateTable();
        }

        private void CreateTable()
        {
            int indentLength = 1;
            table = new Table<Enterprise, FinancialResult>(
                new List<TableColumn<Enterprise>>()
                {
                    new TableColumn<Enterprise>("Предприятие", indentLength,
                      Table<Enterprise>.GetMaxContentLength(Registry.Enterprises.Select(entr =>      entr.Name).ToList()),
                      entr => entr.Name),
                    new TableColumn<Enterprise>("Cум. Доход", indentLength, 20, entr => String.Format($"{entr.SumOfIncome:C2}"), true),
                    new TableColumn<Enterprise>("Cум. Расход", indentLength, 20, entr => String.Format($"{entr.SumOfConsumption:C2}"), true),
                    new TableColumn<Enterprise>("Cум. Прибыли", indentLength, 20, entr => String.Format($"{entr.SumOfProfit:C2}"), true),
                    new TableColumn<Enterprise>("Cум. Рентабельности", indentLength, 20, entr => String.Format($"{entr.SumOfRentability:P2}"), true),
                },
                new List<TableColumn<FinancialResult>>()
                {
                    new TableColumn<FinancialResult>("Год", indentLength, 4, fr => fr.Year.ToString()),
                    new TableColumn<FinancialResult>("Квартал", indentLength, 7, fr => fr.Quarter.ToString()),
                    new TableColumn<FinancialResult>("Доход", indentLength,
                        Table<Enterprise>.GetMaxContentLength(AllFinancialResults.Select(fr =>      String.Format($"{fr.Income:C2}")).ToList()),
                        fr => String.Format($"{fr.Income:C2}")),
                    new TableColumn<FinancialResult>("Расход", indentLength,
                        Table<Enterprise>.GetMaxContentLength(AllFinancialResults.Select(fr =>      String.Format($"{fr.Consumption:C2}")).ToList()),
                        fr => String.Format($"{fr.Consumption:C2}")),
                    new TableColumn<FinancialResult>("Прибыль", indentLength,
                        Table<Enterprise>.GetMaxContentLength(AllFinancialResults.Select(fr => String.Format($"{fr.Profit:C2}")).ToList()),
                        fr => String.Format($"{fr.Profit:C2}")),
                    new TableColumn<FinancialResult>("Рентабельность", indentLength,
                        Table<Enterprise>.GetMaxContentLength(AllFinancialResults.Select(fr => String.Format($"{fr.Rentability:P2}")).ToList()),
                        fr => String.Format($"{fr.Rentability:P2}"))
                });
        }

        private void LoadFromFile()
        {
            try
            {
                var loadedData = SelectFile.LoadFromFile<EnterpriseDTO>();
                if(loadedData != null)
                {
                    Console.WriteLine("Выполняется чтение данных ...");
                    Registry.Enterprises = loadedData.Select(entr => EnterpriseDTO.Map(entr)).ToList();
                    CreateTable();

                    Console.WriteLine("Данные успешно загружены!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка! Файл содержит некорректные данные: " + e.Message);
            }
            finally
            {
                Console.WriteLine($"Для продолжения нажмите любую клавишу ...");
                Console.ReadKey();
            }
        }

        private void SaveToFile()
        {
            try
            {
                SelectFile.SaveToFile(
                    Registry.Enterprises.Select(entr => EnterpriseDTO.Map(entr))
                );
            }
            finally
            {
                Console.WriteLine($"Для продолжения нажмите любую клавишу...");
                Console.ReadKey();
            }
        }

        private void Search()
        {
            Console.Clear();
            Console.WriteLine();
            Console.Write("Поисковый запрос: ");
            SearchBy = Console.ReadLine();

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Нажмите любую кнопку для продолжения ... ");
            Console.ReadKey();
        }

        private void DisableFiltration()
        {
            FilterByQuarter = -1;
            FilterByYear.year = -1;
        }

        private void DisableSorting()
        {
            for(int i = 1; i < table.FisrtColumns.Count; ++i)
            {
                table.FisrtColumns[i].Hidden = true;
            }
            orderBy = (enterprise => enterprise.Name);
        }

        private void FiltersMenuSubPage()
        {
            Console.Clear();
            table.SetWindowSize();

            FilterMenu.PrintSubMenu();

            table.Print(OrderedEnterprises, GetNestedEntity, AmountOfFinancialResults);

            var key = Console.ReadKey().Key;
            FilterMenu.UserChoice(key);
        }

        private void FiltrationByYear()
        {
            Console.Clear();


            Console.WriteLine();
            Console.WriteLine("Настройка фильтра по году");
            Console.WriteLine();
            var years = AllFinancialResults.Select(x => x.Year.year).Distinct().ToList();
            Console.WriteLine("Доступные значения: " + String.Join(";  ",years));

            Console.WriteLine();
            int year = InputValidator.ReadInput(years);
            FilterByYear.year = year;
        }

        private void FiltrationByQuarter()
        {
            Console.Clear();


            Console.WriteLine();
            Console.WriteLine("Настройка фильтра по кварталу");
            Console.WriteLine();
            var quarters = AllFinancialResults.Select(x => x.Quarter).Distinct().ToList();
            Console.WriteLine("Доступные значения: " + String.Join(";  ", quarters));

            Console.WriteLine();
            int quarter = InputValidator.ReadInput(quarters);
            FilterByQuarter = quarter;

        }

        private void SortMenuSubPage()
        {
            Console.Clear();
            table.SetWindowSize();

            SortMenu.PrintSubMenu();

            table.Print(OrderedEnterprises, GetNestedEntity, AmountOfFinancialResults);

            var key = Console.ReadKey().Key;
            SortMenu.UserChoice(key);
        }

        public bool FinancialStatementsPage()
        {
            Console.Clear();
            table.SetWindowSize();

            // Отображение меню 
            FinancialStatementsMenu.PrintSubMenu();

            // Отображение таблицы
            table.Print(OrderedEnterprises, GetNestedEntity, AmountOfFinancialResults);

            // Выбор пользователя 
            var key = Console.ReadKey().Key;
            return FinancialStatementsMenu.UserChoice(key);
        }

        private FinancialResult GetNestedEntity(Enterprise enterprise, int indexOfFR)
        {
            for (int i = 0; i < enterprise.FinancialResults.Count; ++i)
            {
                if (i == indexOfFR)
                    return enterprise.FinancialResults.Values.Select(x => x).ToArray()[i];
            }
            return null;
        }

        private int AmountOfFinancialResults(Enterprise enterprise) =>
            enterprise.FinancialResults.Count;

        public void AddRecord()
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

                int year = InputValidator.ReadYear();
                int quarter = InputValidator.ReadQuarter(year);
                decimal income = InputValidator.ReadIncome();
                decimal consumption = InputValidator.ReadConsumption();

                FinancialResult financialResult = new FinancialResult(year, quarter, income, consumption);
                enterprise.AddFinancialResult(financialResult);

                Console.WriteLine(financialResult);

                Console.WriteLine("Продолжить - любая кнопка  |" + "|  Выход - ESC");
            } while (Console.ReadKey().Key != ConsoleKey.Escape);

            CreateTable();
        }


        private void SortBySumOfIncome()
        {
            orderBy = (enterprise => enterprise.SumOfIncome);
            table.FisrtColumns[1].Hidden = false;
            AutoHidden(1);
        }        
        
        private void SortBySumOfConsumption()
        {
            orderBy = (enterprise => enterprise.SumOfConsumption);
            table.FisrtColumns[2].Hidden = false;
            AutoHidden(2);
        }        
        
        private void SortBySumOfProfit()
        {
            orderBy = (enterprise => enterprise.SumOfProfit);
            table.FisrtColumns[3].Hidden = false;
            AutoHidden(3);
        }        
        
        private void SortBySumOfRentability()
        {
            orderBy = (enterprise => enterprise.SumOfRentability);
            table.FisrtColumns[4].Hidden = false;
            AutoHidden(4);
        }

        private void AutoHidden(int indexOfUnhide)
        {
            for(int i = 1; i < table.FisrtColumns.Count; ++i)
            {
                if (i != indexOfUnhide)
                    table.FisrtColumns[i].Hidden = true;
            }
        }

    }
}
