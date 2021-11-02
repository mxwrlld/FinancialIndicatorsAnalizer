using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleFIA.UserInterface
{

    class TableColumn<T> where T : class
    {
        public int Width { get; set; }
        public string Title { get; set; }
        public bool Hidden { get; set; }
        public int IndentLength { get; set; }
        public int MaxContentLength { get; }
        public bool IsMaxContentGreaterTitle => MaxContentLength > Title.Length;

        public Func<T, string> GetFormattedValue { get; }

        public TableColumn(string title, int indentLength, int maxContentLength, Func<T, string> getFormattedValue, bool hidden = false)
        {
            Title = title;
            IndentLength = indentLength;
            MaxContentLength = maxContentLength;
            Width = maxContentLength > title.Length
                ? maxContentLength + 2 * indentLength : title.Length + 2 * indentLength;
            GetFormattedValue = getFormattedValue;
            Hidden = hidden;
        }

        public void PrintContent(T obj)
        {
            var content = (obj == null ? null : GetFormattedValue(obj));
            if (!Hidden)
            {
                if (content == null)
                {
                    Console.Write(new string(' ', Width));
                }
                else
                {
                    string indent = new string(' ', IndentLength);
                    Console.Write(indent + content
                        + new string(' ', (IsMaxContentGreaterTitle
                                                ? MaxContentLength - content.Length
                                                : Title.Length - content.Length))
                        + indent
                        );
                }
                Console.Write("│");
            }

        }

        //class TableColumn
        //{
        //    public int Width { get; set; }
        //    public string Title { get; set; }
        //    public int IndentLength { get; set; }
        //    public int MaxContentLength { get; }
        //    public bool IsMaxContentGreaterTitle => MaxContentLength > Title.Length;

        //    public TableColumn(string title, int indentLength, int maxContentLength)
        //    {
        //        Title = title;
        //        IndentLength = indentLength;
        //        MaxContentLength = maxContentLength;
        //        Width = maxContentLength > title.Length
        //            ? maxContentLength + 2 * indentLength : title.Length + 2 * indentLength;
        //    }

        //    public void PrintContent(string content, TableColumn column, bool isContentPositionFirst = false)
        //    {
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

        //}

    }
}
