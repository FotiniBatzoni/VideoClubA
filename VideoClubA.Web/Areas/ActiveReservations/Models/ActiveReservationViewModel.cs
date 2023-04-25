using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace VideoClubA.Web.Areas.ActiveReservations.Models
{
    public class ActiveReservation
    {
        [Display(Name = "Τίτλος Ταινίας")]
        public string MovieTitle { get; set; }

        [Display(Name = "Ημερομηνία Επιστροφής")]
        public DateTime ReturnDate { get; set; }

        [Display(Name = "Σχόλια")]
        public string Comment { get; set; }

        [Display(Name = "Κόπια")]
        public string MovieCopyId { get; set;}

        [Display(Name = "Όνομα")]
        public string FirstName { get; set; }

        [Display(Name = "Επίθετο")]
        public string LastName { get; set; }

        public ActiveReservation()
        {
            
        }

        public ActiveReservation(string movieTitle, DateTime returnDate, string comment,
            string movieCopyid, string firstName, string lastName)
        {
            MovieTitle = movieTitle;
            ReturnDate = returnDate;
            Comment = comment;
            MovieCopyId = movieCopyid;
            FirstName = firstName;
            LastName = lastName;

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