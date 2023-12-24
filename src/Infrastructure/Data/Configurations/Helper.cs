using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace CoduTeam.Infrastructure.Data.Configurations;

public static class Helper
{
    public static void ApplyEnumToStringConversion(this ModelBuilder modelBuilder)
    {
        IEnumerable<Type> entityTypes = modelBuilder.Model
            .GetEntityTypes()
            .Select(et => et.ClrType);

        foreach (Type entityType in entityTypes)
        {
            entityType.GetProperties()
                .Where(prop => prop.PropertyType.IsEnum || IsNullableEnum(prop))
                .ToList()
                .ForEach(enumProperty => MapEnumAsString(modelBuilder, entityType, enumProperty));
        }
    }

    private static void MapEnumAsString(ModelBuilder modelBuilder, Type entityType,
        PropertyInfo enumProperty)
    {
        modelBuilder.Entity(entityType)
            .Property(enumProperty.PropertyType, enumProperty.Name)
            .HasConversion<string>();
    }

    private static bool IsNullableEnum(PropertyInfo prop)
    {
        return prop.PropertyType.IsGenericType &&
               prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) &&
               prop.PropertyType.GetGenericArguments()[0].IsEnum;
    }
}
