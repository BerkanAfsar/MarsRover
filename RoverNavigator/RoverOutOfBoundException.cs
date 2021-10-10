using System;

namespace RoverNavigator
{
    public class RoverOutOfBoundException : Exception
    {
        public RoverOutOfBoundException()
     : base()
        { }

        public RoverOutOfBoundException(String message)
            : base(message)

        { }
    }
}
