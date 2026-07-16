using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Base.Infrastructure.Persistence.Extensions
{
    public static class DbExtension
    {
        public static void RegisterEntites<BaseType>(this ModelBuilder modelBuilder, params Assembly[] assemblies)
        {
            IEnumerable<Type> types = assemblies.SelectMany(x => x.GetExportedTypes()).Where(
                c => c.IsClass && !c.IsAbstract && c.IsPublic && typeof(BaseType).IsAssignableFrom(c));

            foreach (var type in types)
                modelBuilder.Entity(type);
        }
    }
}