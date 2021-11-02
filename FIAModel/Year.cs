using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FIAModel
{
    public struct Year
    {
        public int year;

        public Year(int year)
        {
            this.year = year;
        }

        public override string ToString()
        {
            return year.ToString();
        }
    }
}
