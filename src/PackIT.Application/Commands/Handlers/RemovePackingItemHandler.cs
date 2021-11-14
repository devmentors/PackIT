using System.Threading.Tasks;
using PackIT.Application.Exceptions;
using PackIT.Domain.Repositories;
using PackIT.Shared.Abstractions.Commands;

namespace PackIT.Application.Commands.Handlers
{
    internal sealed class RemovePackingItemHandler : ICommandHandler<RemovePackingItem>
    {
        private readonly IPackingListRepository _repository;

        public RemovePackingItemHandler(IPackingListRepository repository)
            => _repository = repository;

        public async Task HandleAsync(RemovePackingItem command)
        {
            var packingList = await _repository.GetAsync(command.PackingListId);

            if (packingList is null)
            {
                throw new PackingListNotFoundException(command.PackingListId);
            }

            packingList.RemoveItem(command.Name);

            await _repository.UpdateAsync(packingList);
        }
    }
}