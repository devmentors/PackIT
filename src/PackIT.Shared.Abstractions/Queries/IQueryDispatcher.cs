using System.Threading.Tasks;

namespace PackIT.Shared.Abstractions.Queries
{
    public interface IQueryDispatcher
    {
        Task<TResult> QueryAsync<TResult>(IQuery<TResult> query);
    }
}