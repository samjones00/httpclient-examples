using App.Models;

namespace App.Interfaces
{
    public interface IMovieReviewService
    {
        Task<string> GetAsync(string name);
        Task<ApiResponse> GetFromJsonAsync(string name);
        Task<string> GetStringAsync(string name);
        Task<string> SendAsync(string name);
    }
}