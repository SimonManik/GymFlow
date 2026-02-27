using System.Text.Json.Serialization;

namespace GymFlow.Models.Wger
{
    public class WgarResponse
    {

        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("results")]
        public List<ExerciseItem> Results { get; set; }


    }
}
