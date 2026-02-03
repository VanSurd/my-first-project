// Фабрика контекста БД
// НУЖНА ТОЛЬКО для команд dotnet ef
// Без неё EF часто не видит DbContext в .NET 6/7/8

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace PeopleSearchSite.Data
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            optionsBuilder.UseSqlite("Data Source=people.db");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
