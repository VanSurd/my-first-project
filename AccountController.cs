// Контроллер для регистрации, логина и выхода пользователя
// Управление пользователями через ASP.NET Core Identity
// Использует ApplicationUser, который расширяет IdentityUser

using Microsoft.AspNetCore.Mvc;                     // Для Controller, IActionResult, атрибутов [HttpPost], [HttpGet]
using Microsoft.AspNetCore.Identity;                // Для UserManager, SignInManager, IdentityUser
using PeopleSearchSite.Models;                      // Для ApplicationUser и LoginViewModel
using System.Threading.Tasks;                        // Для Task, необходимого для async методов

namespace PeopleSearchSite.Controllers
{
    public class AccountController : Controller
    {
        // Сервис для управления входом/выходом пользователей
        private readonly SignInManager<ApplicationUser> _signInManager;

        // Сервис для управления пользователями (создание, поиск, роли и т.д.)
        private readonly UserManager<ApplicationUser> _userManager;

        // Конструктор контроллера, внедряем зависимости через Dependency Injection
        public AccountController(
            UserManager<ApplicationUser> userManager,        // Менеджер пользователей
            SignInManager<ApplicationUser> signInManager)   // Менеджер сессий (логин/логаут)
        {
            _userManager = userManager;           // Сохраняем UserManager в поле класса
            _signInManager = signInManager;       // Сохраняем SignInManager в поле класса
        }

        // GET: /Account/Login
        // Отображает форму входа
        [HttpGet]                                      // Метод доступен через GET-запрос
        public IActionResult Login()
        {
            // Возвращаем пустую форму LoginViewModel
            return View(new LoginViewModel());
        }

        // POST: /Account/Login
        // Обрабатывает попытку входа пользователя
        [HttpPost]                                     // Метод доступен через POST-запрос
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            // Проверка модели на валидность
            if (!ModelState.IsValid)
            {
                ViewBag.Error = "Пожалуйста, заполните все поля корректно.";
                return View(model);                     // Возвращаем форму с введёнными данными
            }

            // Попытка входа с использованием Email и Пароля
            var result = await _signInManager.PasswordSignInAsync(
                model.Email,             // Email пользователя
                model.Password,          // Пароль пользователя
                isPersistent: false,     // Не сохранять куки между сессиями
                lockoutOnFailure: false  // Не блокировать при нескольких ошибках входа
            );

            // Если вход успешен
            if (result.Succeeded)
            {
                // Переходим на страницу поиска
                return RedirectToAction("Index", "Search");
            }

            // Если вход неудачен
            ViewBag.Error = "Неверный логин или пароль";  // Сообщение об ошибке
            return View(model);                           // Возвращаем форму с введёнными данными
        }

        // POST: /Account/Logout
        // Завершает текущую сессию пользователя
        [HttpPost]                                     // Для безопасности используем POST
        public async Task<IActionResult> Logout()
        {
            // Завершаем сессию пользователя через SignInManager
            await _signInManager.SignOutAsync();

            // После выхода перенаправляем на страницу логина
            return RedirectToAction("Login");
        }
    }
}
