using System.Text.Json.Serialization;

namespace App.Models
{
    public class ApiResponse
    {
        public List<Result> Results { get; set; } = new();
    }

    public class Result
    {
        [JsonPropertyName("display_title")]
        public string DisplayTitle { get; set; }
    }
}