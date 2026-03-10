using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GymFlow.Data;
using GymFlow.Models;
using GymFlow.Services;
using Microsoft.AspNetCore.Authorization;

namespace GymFlow.Controllers
{
    [Authorize(Roles = "Admin")] // Only admins can access
    public class WorkoutsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWgerService _wgerService;


        public WorkoutsController(ApplicationDbContext context, IWgerService wgerService)
        {
            _context = context;
            _wgerService = wgerService;
        }


        

        // List all workout plans for a specific member
        public async Task<IActionResult> Index(int memberId)
        {
            var member = await _context.Members
                .Include(m => m.WorkoutPlans)
                .FirstOrDefaultAsync(m => m.Id == memberId);

            if (member == null) return NotFound();

            ViewBag.MemberName = member.FirstName + " " + member.LastName;
            ViewBag.MemberId = memberId;

            return View(member.WorkoutPlans);
        }

        // GET: Create new plan
        public IActionResult Create(int memberId)
        {
            var plan = new WorkoutPlan { MemberId = memberId };
            return View(plan);
        }

        // POST: Save the new plan
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(WorkoutPlan plan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(plan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { memberId = plan.MemberId });
            }
            return View(plan);
        }

        // GET: Display plan details and exercises
        public async Task<IActionResult> Details(int id)
        {
            var plan = await _context.WorkoutPlans
                .Include(p => p.Member)
                .Include(p => p.Exercises)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (plan == null) return NotFound();

            return View(plan);
        }

        // GET: Add exercise to a plan
        public async Task<IActionResult>  AddExercise(int planId)
        {
            var exercise = new WorkoutExercise { WorkoutPlanId = planId };
            var exercises = await _wgerService.GetExercisesAsync();
            ViewBag.Exercises = exercises?.Results;
            return View(exercise);
        }

        // POST: Save exercise to plan
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddExercise(WorkoutExercise exercise)
        {
            if (ModelState.IsValid)
            {
                _context.WorkoutExercises.Add(exercise);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { id = exercise.WorkoutPlanId });
            }

            return View(exercise);
        }
    }
}