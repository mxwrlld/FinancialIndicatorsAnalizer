using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleFIA.UserInterface
{
    class TableColumn
    {
        public int Width { get; set; }
        public string Title { get; set; }
        public int IndentLength { get; set; }
        public int MaxContentLength { get; }
        public bool IsMaxContentGreaterTitle => MaxContentLength > Title.Length;

        public TableColumn(string title, int indentLength, int maxContentLength)
        {
            Title = title;
            IndentLength = indentLength;
            MaxContentLength = maxContentLength;
            Width = maxContentLength > title.Length
                ? maxContentLength + 2 * indentLength : title.Length + 2 * indentLength;
        }

    }
}
