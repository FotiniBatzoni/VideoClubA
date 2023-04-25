
using VideoClubA.Core.Entities;

namespace VideoClubA.Core.Interfaces
{

    public interface IMovieCopyService
    {
        public List<MovieCopy> GetAllAvailabiliy();

        public List<MovieCopy> GetAllUnAvailable();
    }
}
