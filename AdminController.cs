using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using PeopleSearchSite.Data;
using PeopleSearchSite.Models;
using System;
using System.IO;
using System.Linq;


namespace PeopleSearchSite.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public AdminController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index()
        {
            return View(_context.People.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Person person, IFormFile photo)
        {
            if (photo != null && photo.Length > 0)
            {
                string fileName = Guid.NewGuid() + Path.GetExtension(photo.FileName);
                string folder = Path.Combine(_env.WebRootPath, "images/people");
                string path = Path.Combine(folder, fileName);

                using var stream = new FileStream(path, FileMode.Create);
                photo.CopyTo(stream);

                person.PhotoPath = "/images/people/" + fileName;
            }

            _context.People.Add(person);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var person = _context.People.FirstOrDefault(p => p.Id == id);
            if (person == null) return NotFound();
            return View(person);
        }

        [HttpPost]
        public IActionResult Edit(Person person, IFormFile photo)
        {
            var existing = _context.People.FirstOrDefault(p => p.Id == person.Id);
            if (existing == null) return NotFound();

            existing.FirstName = person.FirstName;
            existing.LastName = person.LastName;
            existing.MiddleName = person.MiddleName;
            existing.BirthDate = person.BirthDate;

            if (photo != null && photo.Length > 0)
            {
                string fileName = Guid.NewGuid() + Path.GetExtension(photo.FileName);
                string folder = Path.Combine(_env.WebRootPath, "images/people");
                string path = Path.Combine(folder, fileName);

                using var stream = new FileStream(path, FileMode.Create);
                photo.CopyTo(stream);

                existing.PhotoPath = "/images/people/" + fileName;
            }

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var person = _context.People.FirstOrDefault(p => p.Id == id);
            if (person == null) return NotFound();

            _context.People.Remove(person);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
