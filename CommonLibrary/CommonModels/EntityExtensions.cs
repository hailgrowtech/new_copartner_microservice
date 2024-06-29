using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.CommonModels;
public static class EntityExtensions
{
    public static void PreserveProperties<TEntity>(this DbContext context, TEntity updatedEntity, TEntity originalEntity, params string[] propertyNames) where TEntity : class
    {
        var entry = context.Entry(updatedEntity);
        foreach (var propertyName in propertyNames)
        {
            entry.Property(propertyName).CurrentValue = entry.Property(propertyName).OriginalValue;
            entry.Property(propertyName).IsModified = false;
        }
    }
}

