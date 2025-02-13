using Microsoft.AspNetCore.Mvc;
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

    [HttpGet("api/movies")]
    public async Task<IActionResult> GetAllAsync()
    {
        var movies = await _movieRepository.GetAllAsync();
        return Ok(movies);
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
