using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PackIT.Application.DTO;
using PackIT.Application.Queries;
using PackIT.Infrastructure.EF.Contexts;
using PackIT.Infrastructure.EF.Models;
using PackIT.Shared.Abstractions.Queries;

namespace PackIT.Infrastructure.EF.Queries.Handlers
{
    internal sealed class GetPackingListHandler : IQueryHandler<GetPackingList, PackingListDto>
    {
        private readonly DbSet<PackingListReadModel> _packingLists;

        public GetPackingListHandler(ReadDbContext context)
            => _packingLists = context.PackingLists;

        public Task<PackingListDto> HandleAsync(GetPackingList query)
            => _packingLists
                .Include(pl => pl.Items)
                .Where(pl => pl.Id == query.Id)
                .Select(pl => pl.AsDto())
                .AsNoTracking()
                .SingleOrDefaultAsync();
    }
}