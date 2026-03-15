using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymFlow.Models
{
    [Table("WorkoutDay")]
    public class WorkoutDay
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int MemberId { get; set; }
        public Member? Member { get; set; }

        public List<WorkoutExercise> Exercises { get; set; } = new();
    }
}
