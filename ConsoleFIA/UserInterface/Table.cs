using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleFIA.UserInterface
{
    abstract class Table
    {
        public List<TableColumn> Columns { get; }

        public Table(List<TableColumn> columns)
        {
            Columns = columns;
        }

        abstract public void Print();

        public void PrintHeader()
        {

            Console.Write("┌");
            for (int i = 0; i < Columns.Count; i++)
            {
                if (i > 0)
                    Console.Write("┬");
                Console.Write(new string('─', Columns[i].Width));
            }
            Console.Write("┐");


            Console.WriteLine();


            for (int i = 0; i < Columns.Count; i++)
            {
                if (i == 0)
                    Console.Write("│");
                string indent = new string(' ', Columns[i].IndentLength);
                // Не учитывается, что 
                Console.Write(indent + Columns[i].Title 
                    + (Columns[i].IsMaxContentGreaterTitle 
                        ? new string(' ', Columns[i].MaxContentLength - Columns[i].Title.Length) 
                        : String.Empty)
                    + indent);
                Console.Write("│");
            }


            Console.WriteLine();


            Console.Write("├");
            for (int i = 0; i < Columns.Count; i++)
            {
                if (i > 0)
                    Console.Write("┼");
                Console.Write(new string('─', Columns[i].Width));
            }
            Console.Write("┤");
        }

        public void PrintFooter()
        {
            Console.WriteLine();

            Console.Write("└");
            for (int i = 0; i < Columns.Count; i++)
            {
                if (i > 0)
                    Console.Write("┴");
                Console.Write(new string('─', Columns[i].Width));
            }
            Console.Write("┘");
        }
    
        public void PrintContent(string content, TableColumn column, bool isContentPositionFirst = false)
        {
            if (isContentPositionFirst)
            {
                Console.Write("│");
            }
            if (content == null)
            {
                Console.Write(new string(' ', column.Width));
            }
            else
            {
                string indent = new string(' ', column.IndentLength);
                Console.Write(indent + content
                    + new string(' ', (column.IsMaxContentGreaterTitle
                                            ? column.MaxContentLength - content.Length
                                            : column.Title.Length - content.Length))
                    + indent
                    );
            }
            Console.Write("│");
        }
    }
}
