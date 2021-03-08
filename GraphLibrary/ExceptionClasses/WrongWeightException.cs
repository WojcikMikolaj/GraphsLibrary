using System;

namespace GraphLibrary.ExceptionClasses
{
    class WrongWeightException : Exception
    {
        public WrongWeightException(string message) : base(message)
        {

        }
    }
}
