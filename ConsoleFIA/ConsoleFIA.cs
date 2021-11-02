using ConsoleFIA.Controllers;
using ConsoleFIA.UserInterface;
using System;
/*
 * Третья версия проекта 
 */

namespace ConsoleFIA
{
    class ConsoleFIA
    {
        static RegistryOfEnterprisesController registryController = new RegistryOfEnterprisesController();
        static FinancialStatementsController financialStatementsController = new FinancialStatementsController();


        static void Main()
        {
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
                    while (registryController.RegistryOfEnterprisesPage());
                    break;
                case ConsoleKey.E:
                    while(financialStatementsController.FinancialStatementsPage());
                    break;
                case ConsoleKey.Q:
                    return false;
            }
            return true;
        }
    }
}


