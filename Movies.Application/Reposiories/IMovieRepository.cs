using Movies.Application.Models;

namespace Movies.Application.Reposiories;

public interface IMovieRepository
{
    Task<IEnumerable<Movie>> GetAllAsync();
    
    Task<bool> CreateAsync(Movie movie);
    
    Task<bool> UpdateAsync(Movie movie);
    
    Task<Movie?> GetByIdAsync(Guid id);
    
    Task<bool> DeleteByIdAsync(Guid id);
    
}