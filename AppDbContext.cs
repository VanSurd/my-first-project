// AppDbContext — это мост между C# и базой данных
// Он сообщает Entity Framework, какие таблицы есть в БД

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PeopleSearchSite.Models;

namespace PeopleSearchSite.Data
{
    // DbContext теперь включает таблицы Identity
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // Таблица людей
        public DbSet<Person> People { get; set; }
    }
}
