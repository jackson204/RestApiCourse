using Movies.Application.Models;

namespace Movies.Application.Reposiories;

public class MovieRepository : IMovieRepository
{
    public Task<IEnumerable<Movie>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<bool> CreateAsync(Movie movie)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(Movie movie)
    {
        throw new NotImplementedException();
    }

    public Task<Movie?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
