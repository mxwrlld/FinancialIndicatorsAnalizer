using ConsoleFIA.Controllers;
using ConsoleFIA.UserInterface;
using System;
/*
 * Вторая версия проекта 
 */

namespace ConsoleFIA
{
    class ConsoleFIA
    {
        static void Main()
        {
            RegistryOfEnterprisesController registryController = new RegistryOfEnterprisesController();

            while (UserChoice(registryController)) ;
        }

        static Menu MainMenu = new Menu(new []{
            new MenuItem(ConsoleKey.W, "Реестр предприятий"),
            new MenuItem(ConsoleKey.A, "Добавление записи"),
            new MenuItem(ConsoleKey.Q, "Выход"),
        }); 

        static bool UserChoice(RegistryOfEnterprisesController registryController)
        {
            Console.Clear();
            MainMenu.Print("Финансовая отчётность");
            
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.W:
                    registryController.RegistryTableForm();
                    break;
                case ConsoleKey.A:
                    registryController.FillRecord();
                    break;
                case ConsoleKey.Q:
                    return false;
            }
            return true;
        }
    }
}


