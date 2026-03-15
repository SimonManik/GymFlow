using GymFlow.Models.Wger;

namespace GymFlow.Services
{
    public class WgerService : IWgerService
    {
        private readonly HttpClient _httpClient;

        public WgerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<WgarResponse?> GetExercisesAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<WgarResponse>("exercise/?format=json&language=2&limit=100");
            return response;
        }

        public async Task<ExerciseSearchResult?> SearchExercisesAsync(string term)
        {
            var SearchResult = await _httpClient.GetFromJsonAsync<ExerciseSearchResult>($"exercise/search/?term={term}&language=english&format=json");
            return SearchResult ;
        }

        
        
        
    }
}