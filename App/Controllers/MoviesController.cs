using App.Interfaces;
using App.Models;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers;

[ApiController]
[Route("[controller]")]
public class MoviesController : ControllerBase
{
    private readonly IMovieReviewService _httpService;

    public MoviesController(IMovieReviewService httpService)
    {
        ArgumentNullException.ThrowIfNull(httpService);

        _httpService = httpService;
    }

    [HttpGet("{name}/model")]
    public async Task<ApiResponse> GetModel(string name) => await _httpService.GetFromJsonAsync(name);

    [HttpGet("{name}/json")]
    public async Task<string> Get(string name) => await _httpService.GetAsync(name);
}