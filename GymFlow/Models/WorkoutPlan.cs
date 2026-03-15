using System.ComponentModel.DataAnnotations;

namespace GymFlow.Models
{
    public class WorkoutPlan
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Název plánu")]
        public string Name { get; set; } 

        [Display(Name = "Popis")]
        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Link to member 

        public int MemberId { get; set; }
        public Member? Member { get; set; }

        // List of exercises in this plan
        public List<WorkoutDay> Exercises { get; set; } = new();
    }
}