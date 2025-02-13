using Microsoft.AspNetCore.Mvc;
using Movies.Api.Mapping;
using Movies.Application.Models;
using Movies.Application.Reposiories;
using Movies.Contracts.Requests;

namespace Movies.Api.Controllers;

[ApiController]
public class MoviesController : ControllerBase
{
    private readonly IMovieRepository _movieRepository;

    public MoviesController(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    [HttpPost("api/movies")]
    public async Task<IActionResult> CreateAsync([FromBody] CreateMovieRequest request)
    {
        var movie = request.MapToMovie();
        await _movieRepository.CreateAsync(movie);
        return CreatedAtAction(nameof(Get), new { id = movie.Id }, movie);
    }

    [HttpGet("api/movies")]
    public async Task<IActionResult> GetAllAsync()
    {
        var movies = await _movieRepository.GetAllAsync();
        var moviesResponse = new MoviesResponse()
        {
            Items = movies.Select(r => new MovieResponse()
            {
                Id = r.Id,
                Title = r.Title,
                YearOfRelease = r.YearOfRelease,
                Genres = r.Genres
            })
        };
        return Ok(moviesResponse);
    }

    [HttpGet("api/movies/{id}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var movie = await _movieRepository.GetByIdAsync(id);
        if (movie == null)
        {
            return NotFound();
        }
        return Ok(movie);
    }
    
    [HttpPut("api/movies/{id}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateMovieRequest request)
    {
        var movie = new Movie()
        {
            Id = id,
            Title = request.Title,
            YearOfRelease = request.YearOfRelease,
            Genres = request.Genres.ToList()
        };
        var update = await _movieRepository.UpdateAsync(movie);
        if (!update)
        {
            return NotFound();
        }
        return Ok();
    }
    
    [HttpDelete("api/movies/{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
    {
        var delete = await _movieRepository.DeleteByIdAsync(id);
        if (!delete)
        {
            return NotFound();
        }
        return Ok();
    }
}

public class MoviesResponse
{
    public required IEnumerable<MovieResponse> Items { get; init; } = Enumerable.Empty<MovieResponse>();
}
public class MovieResponse
{
    public required Guid Id { get; init; }
    
    public required string Title { get; init; }

    public required int YearOfRelease { get; init; }

    public required IEnumerable<string> Genres { get; init; } = Enumerable.Empty<string>();
}
