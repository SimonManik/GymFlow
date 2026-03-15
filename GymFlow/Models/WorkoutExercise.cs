using System.ComponentModel.DataAnnotations;

namespace GymFlow.Models
{
    public class WorkoutExercise
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Cvik")]
        public string ExerciseName { get; set; }

        [Display(Name = "Série")]
        public int Sets { get; set; }

        [Display(Name = "Opakování")]
        public int Reps { get; set; }

        [Display(Name = "Poznámka")]
        public string? Note { get; set; }

        public int WorkoutDayId { get; set; }
        public WorkoutDay? WorkoutDay { get; set; }
    }
}