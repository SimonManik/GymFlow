using System.Text.Json.Serialization;

namespace GymFlow.Models.Wger
{
    public class ExerciseItem
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("translations")]
        public List<ExerciseTranslation> Translations { get; set; }
    }
}
