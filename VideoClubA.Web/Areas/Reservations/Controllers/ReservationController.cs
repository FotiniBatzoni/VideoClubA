using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VideoClubA.Core.Entities;
using VideoClubA.Core.Interfaces;
using VideoClubA.Web.Areas.Reservations.Models;

namespace VideoClubA.Web.Areas.Reservations.Controllers
{
 
    public class ReservationController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMovieRentService _rentsDb;
        private readonly IMovieService _moviesDb;
        private readonly IMovieCopyService _movieCopiesDb;

        public ReservationController(IMapper mapper, IMovieRentService rentsDb,
            IMovieService moviesDb, IMovieCopyService movieCopiesDb)
        {
            _rentsDb = rentsDb;
            _mapper = mapper;
            _moviesDb = moviesDb;
            _movieCopiesDb = movieCopiesDb;
        }

        [HttpGet]
        [Area("Reservations")]
        public IActionResult ReservationsPerCustomer( string customerId, string firstName, string lastName, 
            int page = 1, int pageSize = 5)
        {
            return View(PaginateRents(page, pageSize,customerId, firstName,lastName));
        }

        [HttpGet]
        [Area("Reservations")]
        public ActionResult DisplayReservationForm(DisplayReservationFormViewModel reservation)
        {
            List<Movie> allMovies = _moviesDb.GetAllMovies();

            List<MovieCopy> availableMovieCopies = new List<MovieCopy>(); 

            var reservationForm = new DisplayReservationFormViewModel
            {
                AllMovies = allMovies,
                FirstName = reservation.FirstName,
                LastName = reservation.LastName,
                CustomerId = reservation.CustomerId

            };

            return View(reservationForm);
        }

        [HttpGet]
        [Area("Reservations")]
        public JsonResult LoadMovieCopies()
        {
            var movieId = Request.Query["movieId"];

            List<MovieCopy> allMovieCopies = _movieCopiesDb.GetAvailableCopies(movieId);

            var allMovieCopyIds  = allMovieCopies.Select(m => m.Id).ToList();

            return Json(allMovieCopyIds) ;
        }


        [HttpPost]
        [Area("Reservations")]
        public ActionResult CreateReservation(CreateReservationBindingModel reservation)
        {
            reservation.CustomerId = Request.Form["CustomerId"];
            reservation.Comment = Request.Form["Comment"];
            var selectedMovie = Request.Form["SelectedMovie"].ToString();
            var selectedMovieParts = selectedMovie.Split(",");
            reservation.MovieId = selectedMovieParts[0];
            reservation.MovieTitle = selectedMovieParts[1];

              CreateReservation(reservation.MovieId, reservation.MovieTitle,
            reservation.CustomerId, reservation.Comment);

            return RedirectToAction("CustomerPanel", "Customer", new { area = "Customers" });
        }




        private RentsPerCustomerViewModel PaginateRents(int page, int pageSize, string customerId, string firstName, string lastName)
        {
            page = Math.Clamp(page, 1, pageSize);

            int startIndex = (int)((page - 1) * pageSize);

            List<MovieRent> rentsPerCustomer = _rentsDb.GetRentsPerCustomer(customerId);

            int totalPages = (int)Math.Ceiling((double)rentsPerCustomer.Count / pageSize);

            var rents = _mapper.Map<List<RentPerCustomerViewModel>>(rentsPerCustomer.Skip(startIndex).Take(pageSize));

            var rentsViewModel = new RentsPerCustomerViewModel(rents, page, customerId, firstName, lastName);

            rentsViewModel.CurrentPage = page;
            rentsViewModel.TotalPages = totalPages;

            return rentsViewModel;
        }

        private void CreateReservation(string movieId, string movieTitle, string customerId, string comment)
        {
            DateTime today = DateTime.Now;

            List<MovieCopy> availableCopies = _movieCopiesDb.GetAvailableCopies(movieId);

            MovieRent movieRent = new MovieRent()
            {
                MovieTitle = movieTitle,
                //MovieCopyId
                CustomerId = customerId,
                RentDate = today,
                ReturnDate = today.AddDays(7),
                Comment = comment,
            };


        }
    }
}
