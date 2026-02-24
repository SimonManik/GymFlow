using Microsoft.AspNetCore.Mvc;
using GymFlow.Data;
using System.Linq;
namespace GymFlow.Controllers
{
    public class MembersController:Controller
    {
        private readonly ApplicationDbContext _context;

        public MembersController(ApplicationDbContext context) {
            _context = context;
        }

        public IActionResult Index()
        {
            var members = _context.Members.ToList();

            return View(members);
        }

        //Actions for capturing and storing data
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost] //data processing method
        public IActionResult Create(GymFlow.Models.Member member)
        {
            if (!ModelState.IsValid) //Checking whether the form data matches the rules in the model
                                     //If not, return the user back for correction
            {
                return View(member);

            }
            _context.Members.Add(member);

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {

            var member = _context.Members.Find(id);

            if (member != null)
            {
                _context.Members.Remove(member);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
         

            var member = _context.Members.Find(id);


            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        [HttpPost]
        public IActionResult Edit(int id, GymFlow.Models.Member member)
        { //Accepts modified data and writes changes to the database

            if (!ModelState.IsValid) 
            {

                return View(member);

            }

            if (id != member.Id)
            {
                return NotFound();
            }

            _context.Members.Update(member);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}

