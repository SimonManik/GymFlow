using System.Text.Json.Serialization;

namespace GymFlow.Models.Wger
{
    public class ExerciseTranslation
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("language")]
        public int Language { get; set; }
    }
}