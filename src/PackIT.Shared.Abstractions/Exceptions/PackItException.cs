using System;

namespace PackIT.Shared.Abstractions.Exceptions
{
    public abstract class PackItException : Exception
    {
        protected PackItException(string message) : base(message)
        {
        }
    }
}