using FIAModel;
using ConsoleFIA.Mocks;
using ConsoleFIA.UserInterface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleFIA.Controllers
{
    class RegistryOfEnterprisesController
    {
        public RegistryOfEnterprises Registry { get; }
        private List<Enterprise> Enterprises => Registry.Enterprises;

        private readonly Menu RegistryMenu;
        private readonly Menu SortMenu;

        private readonly Table<Enterprise> table;

        private string searchBy = null;
        public string SearchBy { get => searchBy; set => searchBy = value.ToLower(); }
        private Func<Enterprise, object> orderBy = (enterprise => enterprise.Name);
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
            

        public RegistryOfEnterprisesController()
        {
            Registry = new RegistryOfEnterprises();
            Registry.Enterprises = MocksFabric.GetMockEnterprisesReports();
            RegistryMenu = new Menu(new List<MenuItem>()
            {
                new MenuAction(ConsoleKey.A, "Добавить предприятие", AddEnterprise),
                new MenuAction(ConsoleKey.S, "Сортировка", SortMenuSubPage),
                new MenuAction(ConsoleKey.D, "Поиск", Search),
                new MenuClose(ConsoleKey.Q, "Главное меню"),
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
            table = new Table<Enterprise>(new List<TableColumn<Enterprise>>
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

        private void SortMenuSubPage()
        {
            Console.Clear();
            SortMenu.PrintSubMenu();

            RegistryPrint();

            var key = Console.ReadKey().Key;
            SortMenu.UserChoice(key);
        }

        public bool RegistryOfEnterprisesPage()
        {
            Console.Clear();
            RegistryMenu.PrintSubMenu();

            RegistryPrint();

            var key = Console.ReadKey().Key;
            return RegistryMenu.UserChoice(key);
        }

        public void RegistryPrint()
        {
            table.SetWindowSize();

            table.Print(OrderedEnterprises);
        }

        private void AddEnterprise()
        {
            Console.Clear();

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
                }
            }
            else
            {
                string tin = InputValidator.ReadTIN();
                string legalAddress = InputValidator.ReadAddress();

                enterprise = new Enterprise(name, tin, legalAddress);
                Registry.AddEnterprise(enterprise);
                Console.WriteLine(enterprise);
            }

        }

        //private int[] GetMaxContentLengthOfColumns()
        //{
        //    var maxWidthOfColumns = new int[2];

        //    var enterprises = Registry.Enterprises;

        //    int maxLengthOfEnterpriseName = 0,
        //        maxLengthOfAddress = 0;

        //    foreach (var enterprise in enterprises)
        //    {
        //        if (maxLengthOfEnterpriseName < enterprise.Name.Length)
        //            maxLengthOfEnterpriseName = enterprise.Name.Length;

        //        if (maxLengthOfAddress < enterprise.LegalAddress.Length)
        //            maxLengthOfAddress = enterprise.LegalAddress.Length;
        //    }


        //    maxWidthOfColumns[0] = maxLengthOfEnterpriseName;
        //    maxWidthOfColumns[1] = maxLengthOfAddress;
        //    return maxWidthOfColumns;
        //}

    }
}
