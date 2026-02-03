// Program.cs
// Главная точка входа ASP.NET Core приложения (.NET 8)
// Здесь настраивается сервер, сервисы, БД и маршрутизация

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using PeopleSearchSite.Models;
using PeopleSearchSite.Data;


var builder = WebApplication.CreateBuilder(args);

// --------------------
// Подключаем DbContext
// --------------------
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=people.db"));

// --------------------
// Подключаем Identity
// --------------------
builder.Services
    .AddIdentity<ApplicationUser, IdentityRole>()  // <--- важно ApplicationUser
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// --------------------
// MVC
// --------------------
builder.Services.AddControllersWithViews();

var app = builder.Build();

// --------------------
// Middleware
// --------------------
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// --------------------
// Маршруты
// --------------------
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Search}/{action=Index}/{id?}");

app.Run();
