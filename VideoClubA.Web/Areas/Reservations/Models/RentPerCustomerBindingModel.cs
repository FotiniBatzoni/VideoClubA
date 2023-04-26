using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace VideoClubA.Web.Areas.Reservations.Models
{
    public class RentPerCustomerBindingModel
    {
        [Required]
        public string CustomerId { get; set; }

        [Required]
        [Display(Name = "Τίτλος Ταινίας")]
        public string MovieTitle { get; set; }

        [Required]
        [Display(Name = "Ημερομηνία Ενοικίασης")]
        public DateTime RentDate { get; set; }


        [Display(Name = "Σχόλια")]
        public string? Comment { get; set; }

        public RentPerCustomerBindingModel()
        {
            
        }

        public RentPerCustomerBindingModel(string customerId, string movieTitle, DateTime rentDate,  string comment)
        {
            CustomerId = customerId;
            MovieTitle = movieTitle;
            RentDate = rentDate;
            Comment = comment;
        }

    }
}
