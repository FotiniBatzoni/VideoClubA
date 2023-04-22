using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VideoClubA.Core.Entities;
using VideoClubA.Core.Interfaces;
using VideoClubA.Web.Areas.Movies.Models;
using VideoClubA.Web.Profiler;

namespace VideoClubA.Web.Areas.Movies.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMovieService _movieDb;
        private readonly IMovieCopyService _movieCopyDb;

        public MovieController(IMapper mapper, IMovieService movieDb, IMovieCopyService movieCopyDb)
        {
            _mapper = mapper;
            _movieDb = movieDb;
            _movieCopyDb = movieCopyDb;
        }

        [HttpGet]
        [Area("Movies")]
        public ActionResult MovieGallery()
        {
            List<Movie> movies = _movieDb.GetAllMovies();
            Dictionary<string,int> availabilityPerMovie = GetAllAvailabilityPerMovie();


            var viewModels = _mapper.Map<List<MovieWithAvailabilityViewModel>>(movies, opt => opt.Items["AvailabilityPerMovie"] = availabilityPerMovie);


            return View(viewModels);
        }

        private Dictionary<string, int> GetAllAvailabilityPerMovie()
        {
            List<MovieCopy> availableMoviesCopies = _movieCopyDb.GetAllAvailabiliy();

            var movieCopyIds = availableMoviesCopies.GroupBy(c => c.MovieId);

            var result = movieCopyIds.ToDictionary(g => g.Key, g => g.Count());

            return result;
        }
    }
}
