using Microsoft.AspNetCore.Mvc;
using PeopleSearchSite.Data;
using PeopleSearchSite.Models;
using System;
using System.Linq;
using System.Collections.Generic;

namespace PeopleSearchSite.Controllers
{
    public class SearchController : Controller
    {
        // Контекст базы данных
        private readonly AppDbContext _context;

        // Внедрение DbContext
        public SearchController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Search/Index
        public IActionResult Index(
            string lastName,
            string firstName,
            DateTime? birthDate,
            int? ageFrom,
            int? ageTo)
        {
            // Получаем всех людей
            var query = _context.People.AsQueryable();

            // Фильтр по фамилии (без учета регистра)
            if (!string.IsNullOrWhiteSpace(lastName))
            {
                query = query.Where(p =>
                    p.LastName.ToLower().Contains(lastName.ToLower()));
            }

            // Фильтр по имени (без учета регистра)
            if (!string.IsNullOrWhiteSpace(firstName))
            {
                query = query.Where(p =>
                    p.FirstName.ToLower().Contains(firstName.ToLower()));
            }

            // Фильтр по точной дате рождения
            if (birthDate.HasValue)
            {
                query = query.Where(p =>
                    p.BirthDate.Date == birthDate.Value.Date);
            }

            // Фильтр по возрасту ОТ
            if (ageFrom.HasValue)
            {
                var dateTo = DateTime.Today.AddYears(-ageFrom.Value);
                query = query.Where(p => p.BirthDate <= dateTo);
            }

            // Фильтр по возрасту ДО
            if (ageTo.HasValue)
            {
                var dateFrom = DateTime.Today.AddYears(-(ageTo.Value + 1));
                query = query.Where(p => p.BirthDate >= dateFrom);
            }

            // Сортировка по фамилии
            var result = query
                .OrderBy(p => p.LastName)
                .ToList();

            return View(result);
        }

        // GET: /Search/Details/5
        public IActionResult Details(int id)
        {
            // Поиск человека по ID
            var person = _context.People.FirstOrDefault(p => p.Id == id);

            if (person == null)
                return NotFound();

            return View(person);
        }
    }
}
