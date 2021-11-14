using PackIT.Domain.Exceptions;

namespace PackIT.Domain.ValueObjects
{
    public record PackingItem
    {
        public string Name { get; }
        public uint Quantity { get; }
        public bool IsPacked { get; init; }

        public PackingItem(string name, uint quantity, bool isPacked = false)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new EmptyPackingListItemNameException();
            }
            
            Name = name;
            Quantity = quantity;
            IsPacked = isPacked;
        }
    }
}