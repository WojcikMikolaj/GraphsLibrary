using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLibrary.ExceptionClasses
{
    class WrongGraphException: Exception
    {
        public WrongGraphException(string message): base(message)
        {

        }
    }
}
