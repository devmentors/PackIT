using System;
using PackIT.Shared.Abstractions.Commands;

namespace PackIT.Application.Commands
{
    public record RemovePackingList(Guid Id) : ICommand;
}