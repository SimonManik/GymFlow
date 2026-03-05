using System;
using System.Collections.Generic;

namespace GymFlow.Models
{
    public class WorkoutSession
    {
        public int Id { get; set; }

        public int WorkoutPlanId { get; set; }
        public WorkoutPlan? WorkoutPlan { get; set; }

        public int MemberId { get; set; }
        public Member? Member { get; set; }

        public DateTime StartedAt { get; set; } = DateTime.Now;
        public DateTime? EndedAt { get; set; }

        // Entries for each exercise performed in this session
        public List<WorkoutSessionEntry> Entries { get; set; } = new();
    }
}
