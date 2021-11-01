using System;

namespace BoardApp.WebApi.Exceptions
{
    public class AuthorizeException : Exception
    {
        public AuthorizeException()
        {
        }

        public AuthorizeException(string message) : base(message)
        {
        }

        public AuthorizeException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
