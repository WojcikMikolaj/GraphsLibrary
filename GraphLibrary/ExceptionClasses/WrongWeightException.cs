using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLibrary.ExceptionClasses
{
    class WrongWeightException: Exception
    {
        public WrongWeightException(string message): base(message)
        {

        }
    }
}
