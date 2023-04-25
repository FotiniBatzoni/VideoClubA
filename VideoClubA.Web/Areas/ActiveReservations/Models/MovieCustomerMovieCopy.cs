using VideoClubA.Core.Entities;
using VideoClubA.Core.Interfaces;
using VideoClubA.Web.Areas.Customers.Models;

namespace VideoClubA.Web.Areas.ActiveReservations.Models
{
    public class MovieCustomerMovieCopy
    {
        private readonly IMovieRentService _movieRentDb;
        private readonly ICustomerSevice _customerDb;
        private readonly IMovieCopyService _movieCopyDb;

        public MovieCustomerMovieCopy(IMovieRentService movieRentDb, 
            ICustomerSevice customerDb, IMovieCopyService movieCopyDb)
        {
            _movieRentDb = movieRentDb;
            _customerDb = customerDb;
            _movieCopyDb = movieCopyDb;
        }

        public List<ActiveReservationViewModel> Get()
        {
            List<MovieRent> movieRents = _movieRentDb.GetMovieRents();
            List<Customer> customers = _customerDb.GetAllCustomers();
            List<MovieCopy> movieCopies = _movieCopyDb.GetAllUnAvailable();

            var activeReservations = movieRents
                .Where(mr => !mr.MovieCopy.IsAvailable && mr.ReturnDate > DateTime.Now)
               .Select(mr => new ActiveReservationViewModel
               {
                   MovieTitle = mr.MovieTitle,
                   ReturnDate = mr.ReturnDate,
                   Comment = mr.Comment,
                   MovieCopyId = mr.MovieCopy.Id,
                   FirstName = mr.Customer.FirstName,
                   LastName = mr.Customer.LastName
               }).ToList();

                return activeReservations;
        }
    }
}


//var query = _context.MovieRents
//    .Where(mr => !mr.MovieCopy.IsAvailable && mr.ReturnDate > DateTime.Now)
//    .Select(mr => new
//    {
//        mr.MovieTitle,
//        mr.ReturnDate,
//        mr.Comment,
//        CopyId = mr.MovieCopy.Id,
//        FirstName = mr.Customer.FirstName,
//        LastName = mr.Customer.LastName
//    })