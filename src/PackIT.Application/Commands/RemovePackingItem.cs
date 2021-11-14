using System;
using PackIT.Shared.Abstractions.Commands;

namespace PackIT.Application.Commands
{
    public record RemovePackingItem(Guid PackingListId, string Name) : ICommand;
}