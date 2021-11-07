using System;

namespace ConsoleFIA.UserInterface
{
    class MenuItem
    {
        public ConsoleKey Key { get; set; }
        public string Title { get; set; }

        public bool Hidden { get; set; }

        public MenuItem(ConsoleKey key, string title)
        {
            Key = key;
            Title = title;
        }

        public void PrintForMainMenu(int left)
        {
            Console.CursorLeft = left;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(Key);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(" - " + Title);
        }

        public void PrintForSubMenu()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(Key);
            Console.Write(" ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(Title);
        }

    }

    class MenuAction : MenuItem
    {
        public Action Action { get; }

        public MenuAction(ConsoleKey key, string title, Action action, bool hidden = false) : base(key, title)
        {
            Action = action;
            Hidden = hidden;
        }
    }

    class MenuClose : MenuItem
    {
        public MenuClose(ConsoleKey key, string title): base(key, title) {}
    }
}
