using System.Data.Entity;

namespace Methodbrary.System.Data.Entity
{
    public static class EntityExtensions
    {
        public static void LoadChildren<TEntity>(this DbContext context, TEntity entity, string propName)
            where TEntity : class
        {
            context.Entry(entity)
                .Reference(propName)
                .Load();
        }
    }
}