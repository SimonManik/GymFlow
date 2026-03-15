using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GymFlow.Data;
using GymFlow.Models;
using GymFlow.Services;
using Microsoft.AspNetCore.Authorization;

namespace GymFlow.Controllers
{
    [Authorize(Roles = "Admin")]
    public class WorkoutsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWgerService _wgerService;

        public WorkoutsController(ApplicationDbContext context, IWgerService wgerService)
        {
            _context = context;
            _wgerService = wgerService;
        }

        // List all days for a member
        public async Task<IActionResult> Index(int memberId)
        {
            var member = await _context.Members
                .Include(m => m.WorkoutDays)
                .FirstOrDefaultAsync(m => m.Id == memberId);

            if (member == null) return NotFound();

            ViewBag.MemberName = member.FirstName + " " + member.LastName;
            ViewBag.MemberId = memberId;

            return View(member.WorkoutDays);
        }

        // GET: Add day to a member
        public IActionResult AddDay(int memberId)
        {
            var day = new WorkoutDay { MemberId = memberId };
            return View(day);
        }

        // POST: Save new day
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddDay(WorkoutDay day)
        {
            if (ModelState.IsValid)
            {
                _context.WorkoutDays.Add(day);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { memberId = day.MemberId });
            }
            return View(day);
        }

        // GET: Day details with exercises
        public async Task<IActionResult> DayDetails(int id)
        {
            var day = await _context.WorkoutDays
                .Include(d => d.Member)
                .Include(d => d.Exercises)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (day == null) return NotFound();

            return View(day);
        }

        // GET: Add exercise to a day
        public async Task<IActionResult> AddExercise(int dayId)
        {
            var exercise = new WorkoutExercise { WorkoutDayId = dayId };
            var exercises = await _wgerService.GetExercisesAsync();
            ViewBag.Exercises = exercises?.Results;
            return View(exercise);
        }

        // POST: Save exercise to day
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddExercise(WorkoutExercise exercise)
        {
            if (ModelState.IsValid)
            {
                _context.WorkoutExercises.Add(exercise);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(DayDetails), new { id = exercise.WorkoutDayId });
            }
            return View(exercise);
        }

        public async Task<IActionResult> SearchExercises(string term)
        {
            var result = await _wgerService.SearchExercisesAsync(term);
            return Json(result?.Suggestions);
        }
    }
}