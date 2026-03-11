using System.Text.Json.Serialization;

namespace GymFlow.Models.Wger
{
    public class ExerciseSearchResult
    {
        [JsonPropertyName("suggestions")]
        public List<ExerciseSuggestion> Suggestions { get; set; }
    }
    
    public class ExerciseSuggestion
    {
        [JsonPropertyName("value")]
        public string Value { get; set; }
    }
}