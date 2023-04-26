using System.ComponentModel.DataAnnotations;
using VideoClubA.Core.Entities;

namespace VideoClubA.Web.Areas.Reservations.Models
{
    public class CreateReservationBindingModel
    {
        public List<Movie> AllMovies;

        public List<MovieCopy> Copies;

        public string CustomerId { get; set; }

        public string FirstName { get; set; }

        public string? LastName { get; set; }

        public string MovieId { get; set; }

        public string MovieTitle { get; set; }

        public string SelectedMovie { get; set; }

        public string MovieCopyId { get; set; }
        public string Comment { get; set;}


        public CreateReservationBindingModel()
        {

        }

        public CreateReservationBindingModel(List<Movie> allMovies, List<MovieCopy> Copies, string firstName, string lastName,
            string movieTitle, string customerId,  string comment, string movieId, string selectedMovie,
            string movieCopyId)
        {
            AllMovies = new List<Movie>();
            Copies = new List<MovieCopy>();    
            MovieTitle = movieTitle;
            CustomerId = customerId;
            Comment = comment;
            FirstName = firstName;
            LastName = lastName;
            MovieId = movieId;
            SelectedMovie = selectedMovie;
            MovieCopyId = movieCopyId;
        }
    }


}
