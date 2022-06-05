using System.ComponentModel.DataAnnotations;

namespace App.Models
{
    public class ApiOptions
    {
        public const string SectionName = "MovieReviews";

        [Required]
        public string BaseUrl { get; set; } = string.Empty;

        [Required]
        public string MovieReviewsEndpoint { get; set; } = string.Empty;

        [Required]
        public string ApiKey { get; set; } = string.Empty;
    }
}