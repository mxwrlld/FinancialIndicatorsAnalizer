using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleFIA.UserInterface
{
    //abstract class Table
    //{
    //    public List<TableColumn> Columns { get; }

    //    public Table(List<TableColumn> columns)
    //    {
    //        Columns = columns;
    //    }

    //    abstract public void Print();

    //    public void PrintHeader()
    //    {

    //        Console.Write("┌");
    //        for (int i = 0; i < Columns.Count; i++)
    //        {
    //            if (i > 0)
    //                Console.Write("┬");
    //            Console.Write(new string('─', Columns[i].Width));
    //        }
    //        Console.Write("┐");


    //        Console.WriteLine();


    //        for (int i = 0; i < Columns.Count; i++)
    //        {
    //            if (i == 0)
    //                Console.Write("│");
    //            string indent = new string(' ', Columns[i].IndentLength);
    //            // Не учитывается, что 
    //            Console.Write(indent + Columns[i].Title
    //                + (Columns[i].IsMaxContentGreaterTitle
    //                    ? new string(' ', Columns[i].MaxContentLength - Columns[i].Title.Length)
    //                    : String.Empty)
    //                + indent);
    //            Console.Write("│");
    //        }


    //        Console.WriteLine();


    //        Console.Write("├");
    //        for (int i = 0; i < Columns.Count; i++)
    //        {
    //            if (i > 0)
    //                Console.Write("┼");
    //            Console.Write(new string('─', Columns[i].Width));
    //        }
    //        Console.Write("┤");
    //    }

    //    public void PrintFooter()
    //    {
    //        Console.WriteLine();

    //        Console.Write("└");
    //        for (int i = 0; i < Columns.Count; i++)
    //        {
    //            if (i > 0)
    //                Console.Write("┴");
    //            Console.Write(new string('─', Columns[i].Width));
    //        }
    //        Console.Write("┘");
    //    }

    //    public void PrintContent(string content, TableColumn column, bool isContentPositionFirst = false)
    //    {
    //        if (isContentPositionFirst)
    //        {
    //            Console.Write("│");
    //        }
    //        if (content == null)
    //        {
    //            Console.Write(new string(' ', column.Width));
    //        }
    //        else
    //        {
    //            string indent = new string(' ', column.IndentLength);
    //            Console.Write(indent + content
    //                + new string(' ', (column.IsMaxContentGreaterTitle
    //                                        ? column.MaxContentLength - content.Length
    //                                        : column.Title.Length - content.Length))
    //                + indent
    //                );
    //        }
    //        Console.Write("│");
    //    }

    //    public static int GetMaxContentLength(List<string> content)
    //    {
    //        int maxContentLength = 0;

    //        foreach (var unit in content)
    //        {
    //            if (maxContentLength < unit.Length)
    //                maxContentLength = unit.Length;
    //        }

    //        return maxContentLength;
    //    }

    //    public void UpdateWidthOfColumns()
    //    {
    //        foreach (var column in Columns)
    //        {

    //        }
    //    }
    //}

    class Table<T> where T : class
    {
        public List<TableColumn<T>> Columns { get; }

        public Table(List<TableColumn<T>> columns)
        {
            Columns = columns;
        }

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
            Console.Write("└");
            for (int i = 0; i < Columns.Count; i++)
            {
                if (i > 0)
                    Console.Write("┴");
                Console.Write(new string('─', Columns[i].Width));
            }
            Console.Write("┘");
        }

        public static int GetMaxContentLength(List<string> content)
        {
            int maxContentLength = 0;

            foreach (var unit in content)
            {
                if (maxContentLength < unit.Length)
                    maxContentLength = unit.Length;
            }

            return maxContentLength;
        }

        public void UpdateWidthOfColumns()
        {
            foreach (var column in Columns)
            {

            }
        }

        public void Print(IEnumerable<T> rows)
        {
            PrintHeader();
            Console.WriteLine();

            foreach (var row in rows)
            {
                Console.Write("│");
                foreach (var column in Columns)
                {
                    column.PrintContent(row);
                }

                Console.WriteLine();
            }
            PrintFooter();
        }


        public void SetWindowSize()
        {
            int maxWindowWidth = 6; // Количество соединительных символов вроде "┌", "┐" и т.д.
            foreach (var column in Columns)
            {
                if (!column.Hidden)
                    maxWindowWidth += column.Width;
            }

            Console.SetWindowSize(maxWindowWidth, 20);
        }
    }

    class Table<T, K>
        where T : class
        where K : class
    {
        public List<TableColumn<T>> FisrtColumns { get; }
        public List<TableColumn<K>> OtherColumns { get; }

        public Table(List<TableColumn<T>> fisrtColumns, List<TableColumn<K>> otherColumns)
        {
            FisrtColumns = fisrtColumns;
            OtherColumns = otherColumns;
        }

        public void PrintHeader()
        {

            Console.Write("┌");
            for (int i = 0; i < FisrtColumns.Count; i++)
            {
                if (!FisrtColumns[i].Hidden)
                {
                    if (i > 0)
                        Console.Write("┬");
                    Console.Write(new string('─', FisrtColumns[i].Width));
                }

            }
            for (int i = 0; i < OtherColumns.Count; i++)
            {
                if (!OtherColumns[i].Hidden)
                {
                    Console.Write("┬");
                    Console.Write(new string('─', OtherColumns[i].Width));
                }

            }
            Console.Write("┐");


            Console.WriteLine();


            for (int i = 0; i < FisrtColumns.Count; i++)
            {
                if (!FisrtColumns[i].Hidden)
                {
                    if (i == 0)
                        Console.Write("│");
                    string indent = new string(' ', FisrtColumns[i].IndentLength);
                    // Не учитывается, что 
                    Console.Write(indent + FisrtColumns[i].Title
                        + (FisrtColumns[i].IsMaxContentGreaterTitle
                            ? new string(' ', FisrtColumns[i].MaxContentLength - FisrtColumns[i].Title.Length)
                            : String.Empty)
                        + indent);
                    Console.Write("│");
                }
            }
            for (int i = 0; i < OtherColumns.Count; i++)
            {
                if (!OtherColumns[i].Hidden)
                {
                    string indent = new string(' ', OtherColumns[i].IndentLength);
                    // Не учитывается, что 
                    Console.Write(indent + OtherColumns[i].Title
                        + (OtherColumns[i].IsMaxContentGreaterTitle
                            ? new string(' ', OtherColumns[i].MaxContentLength - OtherColumns[i].Title.Length)
                            : String.Empty)
                        + indent);
                    Console.Write("│");
                }
            }


            Console.WriteLine();


            Console.Write("├");
            for (int i = 0; i < FisrtColumns.Count; i++)
            {
                if (!FisrtColumns[i].Hidden)
                {
                    if (i > 0)
                        Console.Write("┼");
                    Console.Write(new string('─', FisrtColumns[i].Width));
                }
            }
            for (int i = 0; i < OtherColumns.Count; i++)
            {
                if (!OtherColumns[i].Hidden)
                {
                    Console.Write("┼");
                    Console.Write(new string('─', OtherColumns[i].Width));
                }

            }
            Console.Write("┤");
        }

        public void PrintFooter()
        {
            Console.Write("└");
            for (int i = 0; i < FisrtColumns.Count; i++)
            {
                if (!FisrtColumns[i].Hidden)
                {
                    if (i > 0)
                        Console.Write("┴");
                    Console.Write(new string('─', FisrtColumns[i].Width));
                }
            }
            for (int i = 0; i < OtherColumns.Count; i++)
            {
                if (!OtherColumns[i].Hidden)
                {
                    Console.Write("┴");
                    Console.Write(new string('─', OtherColumns[i].Width));
                }
            }
            Console.Write("┘");
        }

        public static int GetMaxContentLength(List<string> content)
        {
            int maxContentLength = 0;

            foreach (var unit in content)
            {
                if (maxContentLength < unit.Length)
                    maxContentLength = unit.Length;
            }

            return maxContentLength;
        }


        public void Print(IEnumerable<T> rows)
        {
            PrintHeader();
            Console.WriteLine();

            foreach (var row in rows)
            {
                Console.Write("│");
                foreach (var column in FisrtColumns)
                {
                    column.PrintContent(row);
                }

                Console.WriteLine();
            }
            PrintFooter();
        }


        // Для вложенных структур
        public void Print(IEnumerable<T> rows, Func<T, int, K> getNestedEntity, Func<T, int> getAmountOfChild)
        {
            PrintHeader();
            Console.WriteLine();

            var entities = rows; // Для лучшего понимания происходящего 

            foreach (var entity in entities)
            {

                Console.Write("│");
                foreach (var column in FisrtColumns)
                {
                    column.PrintContent(entity);
                }
                foreach (var column in OtherColumns)
                {
                    column.PrintContent(getNestedEntity(entity, 0));
                }
                Console.WriteLine();

                int amountOfChild = getAmountOfChild(entity);
                for (int i = 1; i < amountOfChild; ++i)
                {
                    Console.Write("│");
                    foreach (var column in FisrtColumns)
                    {
                        column.PrintContent(null);
                    }
                    foreach (var column in OtherColumns)
                    {
                        column.PrintContent(getNestedEntity(entity, i));
                    }
                    Console.WriteLine();
                }
            }

            PrintFooter();
        }


        public void SetWindowSize()
        {
            int maxWindowWidth = 6; // Количество соединительных символов вроде "┌", "┐" и т.д.
            foreach (var column in FisrtColumns)
            {
                if(!column.Hidden)
                    maxWindowWidth += column.Width;
            }            
            foreach (var column in OtherColumns)
            {
                if (!column.Hidden)
                    maxWindowWidth += column.Width;
            }

            Console.SetWindowSize(maxWindowWidth, 20);
        }

    }

}
