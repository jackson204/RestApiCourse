using Microsoft.AspNetCore.Mvc;
using Movies.Application.Models;
using Movies.Application.Reposiories;

namespace Movies.Api.Controllers;

[ApiController]
public class MoviesController : ControllerBase
{
    private IMovieRepository _movieRepository;

    [HttpPost("api/movies")]
    public async Task<IActionResult> CreateAsync([FromBody] CreateMovieRequest request)
    {
        var movie = new Movie
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            YearOfRelease = request.YearOfRelease,
            Genres = request.Genres.ToList()
        };
        await _movieRepository.CreateAsync(movie);
        return Ok();
    }
}

public class CreateMovieRequest
{
    public required string Title { get; init; }

    public required int YearOfRelease { get; init; }

    public required IEnumerable<string> Genres { get; init; } = Enumerable.Empty<string>();
}
