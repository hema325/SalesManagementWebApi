using Microsoft.EntityFrameworkCore;

namespace Application.Common.Extensions
{
    internal static class IQueryableExtensions
    {
        public static async Task<PaginatedList<TEntity>> PaginateAsync<TEntity>(this IQueryable<TEntity> query,int pageNumber, int pageSize) where TEntity : class
        {
            var totalCount = await query.CountAsync();
            var data = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginatedList<TEntity>(data, totalCount, pageNumber, pageSize);
        }
    }
}
