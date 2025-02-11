using Movies.Application.Models;

namespace Movies.Application.Reposiories;

public class MovieRepository : IMovieRepository
{
    private static readonly List<Movie> _movies = [];

    public Task<IEnumerable<Movie>> GetAllAsync()
    {
        return Task.FromResult(_movies.AsEnumerable());
    }

    public Task<bool> CreateAsync(Movie movie)
    {
        _movies.Add(movie);
        return Task.FromResult(true);
    }

    public Task<bool> UpdateAsync(Movie movie)
    {
        var index = _movies.FindIndex(r => r.Id == movie.Id);
        if (index == -1)
        {
            return Task.FromResult(false);
        }
        _movies[index] = movie;
        return Task.FromResult(true);
    }

    public Task<Movie?> GetByIdAsync(Guid id)
    {
        var movie = _movies.FirstOrDefault(r => r.Id == id);
        return Task.FromResult(movie);
    }

    public Task<bool> DeleteByIdAsync(Guid id)
    {
        var result = _movies.RemoveAll(r => r.Id == id);
        return Task.FromResult(result > 0);
    }
}
