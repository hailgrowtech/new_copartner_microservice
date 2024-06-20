using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CommonLibrary.Extensions;
public static class DateTimeExtensions
{
    private static readonly TimeZoneInfo _istTimeZone = TimeZoneInfo.FindSystemTimeZoneById(
        OperatingSystem.IsWindows() ? "India Standard Time" : "Asia/Kolkata");

    public static DateTime ToIST(this DateTime utcDateTime)
    {
        return TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, _istTimeZone);
    }

    public static T ConvertAllDateTimesToIST<T>(this T entity) where T : class
    {
        if (entity == null) return null;

        var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                  .Where(p => p.PropertyType == typeof(DateTime) ||
                                              p.PropertyType == typeof(DateTime?));

        foreach (var property in properties)
        {
            if (property.PropertyType == typeof(DateTime))
            {
                var utcDateTime = (DateTime)property.GetValue(entity);
                property.SetValue(entity, utcDateTime.ToIST());
            }
            else if (property.PropertyType == typeof(DateTime?))
            {
                var nullableUtcDateTime = (DateTime?)property.GetValue(entity);
                if (nullableUtcDateTime.HasValue)
                {
                    property.SetValue(entity, nullableUtcDateTime.Value.ToIST());
                }
            }
        }

        return entity;
    }
}
