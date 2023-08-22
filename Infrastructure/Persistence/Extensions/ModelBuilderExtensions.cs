using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Infrastructure.Persistence.Extensions
{
    internal static class ModelBuilderExtensions
    {
        public static ModelBuilder AppendGlobalQueryFilter<TContract>(this ModelBuilder builder,Expression<Func<TContract,bool>> filter)
        {
            var entities = builder.Model.GetEntityTypes().Where(e => e.ClrType.IsAssignableTo(typeof(TContract)));

            foreach(var entity in entities)
            {
                var param = Expression.Parameter(entity.ClrType, "e");
                var body = ReplacingExpressionVisitor.Replace(filter.Parameters.Single(), param, filter.Body);

                if(entity.GetQueryFilter() is { } existingFilter)
                {
                    var existingBody = ReplacingExpressionVisitor.Replace(existingFilter.Parameters.Single(), param, existingFilter.Body);
                    body = Expression.AndAlso(body, existingBody);
                }

                entity.SetQueryFilter(Expression.Lambda(body, param));
            }

            return builder;
        }
    }
}
