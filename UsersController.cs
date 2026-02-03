// Контроллер для управления пользователями
// Доступен ТОЛЬКО администраторам

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PeopleSearchSite.Models;
using System.Linq;
using System.Threading.Tasks;

namespace PeopleSearchSite.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        
        // Внедряем менеджеры пользователей и ролей
        public UsersController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // Список пользователей
        public IActionResult Index()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }

        // Форма создания пользователя
        public IActionResult Create()
        {
            return View();
        }

        // Обработка создания пользователя
        [HttpPost]
        public async Task<IActionResult> Create(
            string email,
            string password,
            string role)
        {
            if (string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(password) ||
                string.IsNullOrEmpty(role))
            {
                ViewBag.Error = "Все поля обязательны";
                return View();
            }

            var user = new ApplicationUser
            {
                UserName = email,
                Email = email
            };

            // Создаём пользователя
            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                ViewBag.Error = string.Join(", ",
                    result.Errors.Select(e => e.Description));
                return View();
            }

            // Назначаем роль
            await _userManager.AddToRoleAsync(user, role);

            return RedirectToAction("Index");
        }
    }
}
