using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleFIA.UserInterface
{
    class Menu
    {
        IEnumerable<MenuItem> Items { get; set; }

        public Menu(IEnumerable<MenuItem> items)
        {
            Items = items;
        }

        public bool UserChoice(ConsoleKey key)
        {
            foreach(var item in Items)
            {
                if(item.Key == key)
                {
                    if (item is MenuAction)
                        (item as MenuAction)?.Action();
                    if (item is MenuClose)
                        return false;
                }
            }
            return true;
        }

        public void PrintMainMenu(string programTitle)
        {
            int height = 7, width = 40;
            int left = (Console.WindowWidth - width) / 2;
            int top = (Console.WindowHeight - height) / 2;
            PrintMenuFrame(height, width, left, top);

            Console.WriteLine("        " + programTitle);
            foreach (var item in Items)
            {
                item.PrintForMainMenu(left + 2);
            }

            Console.ResetColor();
        }

        public void PrintSubMenu(ConsoleColor consoleColor = ConsoleColor.DarkCyan)
        {
            Console.BackgroundColor = consoleColor;
            foreach (var item in Items)
            {
                if (!item.Hidden)
                {
                    item.PrintForSubMenu();
                    Console.Write("  ");
                }
            }
            if(Console.WindowWidth > Console.CursorLeft)
            {
                Console.Write(new string(' ', Console.WindowWidth - Console.CursorLeft));
            }
            Console.ResetColor();
        }

        private static void PrintMenuFrame(int height, int width, int left, int top)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            string horisontalLine = new string('═', width - 2);
            string line = $"║{new string(' ', width - 2)}║\n";
            Console.SetCursorPosition(left, top);
            Console.Write($"╔{horisontalLine}╗");
            for (int i = 1; i < height - 1; i++)
            {
                Console.SetCursorPosition(left, top + i);
                Console.Write(line);
            }
            Console.SetCursorPosition(left, top + height - 1);
            Console.Write($"╚{new string('═', width - 2)}╝");
            Console.ResetColor();
            Console.SetCursorPosition(left + 1, top + 1);
        }
    }
}
