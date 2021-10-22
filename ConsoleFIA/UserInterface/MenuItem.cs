using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleFIA.UserInterface
{
    class MenuItem
    {
        public ConsoleKey Key { get; set; }
        public string Title { get; set; }

        public MenuItem(ConsoleKey key, string title)
        {
            Key = key;
            Title = title;
        }

        public void Print(int left)
        {
            Console.CursorLeft = left;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(Key);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(" - " + Title);
        }
    }
}
