using Microsoft.AspNetCore.Mvc;
using PeopleSearchSite.Data;
using System.Linq;

namespace PeopleSearchSite.Controllers
{
    public class ProfileController : Controller
    {
        private readonly AppDbContext _context;

        public ProfileController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Profile/Details/{id}
        public IActionResult Details(int id)
        {
            var person = _context.People.FirstOrDefault(p => p.Id == id);
            if (person == null)
                return NotFound();

            return View(person);
        }
    }
}
