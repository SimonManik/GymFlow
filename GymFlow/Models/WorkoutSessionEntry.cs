using System;

namespace GymFlow.Models
{
    public class WorkoutSessionEntry
    {
        public int Id { get; set; }

        public int WorkoutSessionId { get; set; }
        public WorkoutSession? WorkoutSession { get; set; }

        // Optional link to the plan's WorkoutExercise
        public int? WorkoutExerciseId { get; set; }
        public WorkoutExercise? WorkoutExercise { get; set; }
        // Store the exercise name to keep a record even if the referenced exercise is changed/deleted
        public string ExerciseName { get; set; }

        public int SetsCompleted { get; set; }
        public int RepsCompleted { get; set; }

        public string? Note { get; set; }
    }
}
