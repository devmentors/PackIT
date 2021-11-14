using PackIT.Domain.Entities;
using PackIT.Domain.ValueObjects;
using PackIT.Shared.Abstractions.Domain;

namespace PackIT.Domain.Events
{
    public record PackingItemPacked(PackingList PackingList, PackingItem PackingItem) : IDomainEvent;
}