using GymFlow.Models.Wger;

namespace GymFlow.Services
{
    public interface IWgerService
    {
        Task<WgarResponse?> GetExercisesAsync();
        
        Task<ExerciseSearchResult?> SearchExercisesAsync(string term);

    }
    

    
}