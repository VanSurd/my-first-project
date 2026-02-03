using Microsoft.AspNetCore.Identity;
using PeopleSearchSite.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PeopleSearchSite.Data
{
    public static class DbInitializer
    {
        // 1️⃣ Метод для создания ролей и первого администратора
        public static async Task InitializeAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // создаём роли
            string[] roles = new[] { "Admin", "User" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            // создаём администратора
            string adminEmail = "admin@example.com";
            string adminPassword = "Admin123";

            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FullName = "Администратор"
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }

        // 2️⃣ Метод для добавления тестовых людей
        public static async Task SeedPeopleAsync(AppDbContext context)
        {
            if (!context.People.Any())
            {
                context.People.AddRange(
                    new Person
                    {
                        FirstName = "Иван",
                        LastName = "Иванов",
                        MiddleName = "Иванович",
                        BirthDate = new DateTime(1990, 5, 12),
                        PhotoPath = "/images/people/ivanov.jpg"
                    },
                    new Person
                    {
                        FirstName = "Мария",
                        LastName = "Петрова",
                        MiddleName = "Сергеевна",
                        BirthDate = new DateTime(1985, 8, 30),
                        PhotoPath = "/images/people/petrova.jpg"
                    }
                );

                await context.SaveChangesAsync();
            }
        }
    }
}
