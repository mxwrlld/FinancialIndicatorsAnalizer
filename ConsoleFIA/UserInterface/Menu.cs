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

        public void Print(string programTitle)
        {
            int height = 7, width = 40;
            int left = (Console.WindowWidth - width) / 2;
            int top = (Console.WindowHeight - height) / 2;
            PrintMenuFrame(height, width, left, top);

            Console.WriteLine("        " + programTitle);
            foreach (var item in Items)
            {
                item.Print(left + 2);
            }

            Console.ResetColor();
        }

        static void PrintMenuFrame(int height, int width, int left, int top)
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
