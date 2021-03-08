using System;

namespace GraphLibrary.ExceptionClasses
{
    class WrongGraphException : Exception
    {
        public WrongGraphException(string message) : base(message)
        {

        }
    }
}
