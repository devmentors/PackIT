using System;
using PackIT.Domain.Consts;
using PackIT.Shared.Abstractions.Commands;

namespace PackIT.Application.Commands
{
    public record CreatePackingListWithItems(Guid Id, string Name, ushort Days, Gender Gender,
        LocalizationWriteModel Localization) : ICommand;

    public record LocalizationWriteModel(string City, string Country);
}