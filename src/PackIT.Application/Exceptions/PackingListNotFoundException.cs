using System;
using PackIT.Shared.Abstractions.Exceptions;

namespace PackIT.Application.Exceptions
{
    public class PackingListNotFoundException : PackItException
    {
        public Guid Id { get; }

        public PackingListNotFoundException(Guid id) : base($"Packing list with ID '{id}' was not found.")
            => Id = id;
    }
}