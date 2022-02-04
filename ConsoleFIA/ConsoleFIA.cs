using ConsoleFIA.Controllers;
using ConsoleFIA.UserInterface;
using ConsoleFIA.DB;
using System;
using System.Text;

namespace ConsoleFIA
{
    class ConsoleFIA
    {
        static RegistryController financialStatementsController = new RegistryController();


        static void Main()
        {
            Console.OutputEncoding = Encoding.Unicode;
            while (UserChoice());
        }

        static Menu MainMenu = new Menu(new []{
            new MenuItem(ConsoleKey.W, "Реестр предприятий"),
            new MenuItem(ConsoleKey.E, "Финансовая отчётность"),
            new MenuItem(ConsoleKey.Q, "Выход"),
        }); 

        static bool UserChoice()
        {
            Console.Clear();
            MainMenu.PrintMainMenu("Финансовая отчётность");
            
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.W:
                    while (financialStatementsController.RegistryOfEnterprisesPage());
                    break;
                case ConsoleKey.E:
                    while(financialStatementsController.FinancialStatementsPage());
                    break;
                case ConsoleKey.Q:
                    Console.Clear();
                    DbManager.Updateenterprises(financialStatementsController.Registry.Enterprises);
                    return false;
            }
            return true;
        }
    }
}


