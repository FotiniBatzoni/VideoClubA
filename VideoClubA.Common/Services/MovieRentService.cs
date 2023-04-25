using VideoClubA.Core.Interfaces;
using VideoClubA.Infrastucture.Data;
using VideoClubA.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace VideoClubA.Common.Services
{
    public class MovieRentService : IMovieRentService
    {
        private readonly VideoClubDbContext _context;

        public MovieRentService(VideoClubDbContext context)
        {
            _context = context;
        }

        public List<MovieRent> GetMovieRents()
        {
            return _context.MovieRents.
                Where(mr => mr.ReturnDate > DateTime.Now)
                .AsNoTracking()
                .ToList();

        }
    }
}




