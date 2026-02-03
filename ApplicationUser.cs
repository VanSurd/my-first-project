// ApplicationUser — это расширение стандартного пользователя Identity
// Здесь можно добавлять свои поля, например, ФИО, фото и т.д.

using Microsoft.AspNetCore.Identity;

namespace PeopleSearchSite.Models
{
    public class ApplicationUser : IdentityUser
    {
        // Пример дополнительного поля: полное имя
        public string FullName { get; set; } = string.Empty;
    }
}
