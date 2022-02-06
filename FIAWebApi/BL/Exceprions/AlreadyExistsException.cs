using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FIAWebApi.BL.Exceprions
{
    public class AlreadyExistsException : Exception
    {
        public AlreadyExistsException(string message) : base(message) { }
    }
}
