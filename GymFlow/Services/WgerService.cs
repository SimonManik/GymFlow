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
        
    }
}