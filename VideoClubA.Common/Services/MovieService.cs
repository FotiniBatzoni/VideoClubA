
using VideoClubA.Core.Entities;
using VideoClubA.Core.Interfaces;
using VideoClubA.Infrastucture.Data;

namespace VideoClubA.Common.Services
{
    public class MovieService : IMovieService
    {
        private readonly VideoClubDbContext _context;

        public MovieService(VideoClubDbContext context)
        {
            _context = context;
        }
        public List<Movie> GetAllMovies()
        {
          return (from movies in _context.Movies
                        select movies).ToList();
        }


    }
}
