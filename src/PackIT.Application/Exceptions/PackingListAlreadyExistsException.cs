using PackIT.Shared.Abstractions.Exceptions;

namespace PackIT.Application.Exceptions
{
    public class PackingListAlreadyExistsException : PackItException
    {
        public string Name { get; }

        public PackingListAlreadyExistsException(string name) 
            : base($"Packing list with name '{name}' already exists.")
        {
            Name = name;
        }
    }
}