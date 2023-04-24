using VideoClubA.Core.Entities;
using VideoClubA.Core.Interfaces;
using VideoClubA.Infrastucture.Data;

namespace VideoClubA.Common.Services
{
    public class MovieCopyService : IMovieCopyService
    {
        private readonly VideoClubDbContext _context;

        public MovieCopyService(VideoClubDbContext context)
        {
            _context = context;
        }
        public List<MovieCopy> GetAllAvailabiliy()
        {
            return _context.MovieCopies
                        .Where(moviecopies => moviecopies.IsAvailable == true)
                        .ToList();
        }
    }
}
