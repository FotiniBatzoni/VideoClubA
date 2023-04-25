using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Drawing.Printing;
using VideoClubA.Core.Interfaces;
using VideoClubA.Web.Areas.Customers.Models;
using VideoClubA.Web.Areas.Movies.Controllers;
using VideoClubA.Web.Areas.Movies.Models;

namespace VideoClubA.Web.Areas.Customers.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly ICustomerSevice _customerDb;
        private readonly IMovieRentService _movieRentDb;

        public CustomerController(IMapper mapper, ICustomerSevice customerDb, IMovieRentService movieRentDb,
            ILogger<MovieController> logger)
        {
            _mapper = mapper;
            _logger = logger;
            _customerDb = customerDb;
            _movieRentDb = movieRentDb;

        }


        [HttpGet]
        [Area("Customers")]
        public ActionResult CustomerPanel(int page = 1, int pageSize = 5)
        {
            return View(PaginateCustomer(page, pageSize));
        }

        private CustomersWithActiveReservationViewModel PaginateCustomer(int page, int pageSize)
        {

            //Validate Page
            page = Math.Clamp(page, 1, pageSize);

            int startIndex = (int)((page - 1) * pageSize);

            var customersWithActiveReservations = new CustomersWithActiveReservationViewModel(_customerDb, _movieRentDb).Get();

            int totalPages = (int)Math.Ceiling((double)customersWithActiveReservations.Count / pageSize);
            
            customersWithActiveReservations = customersWithActiveReservations.Skip(startIndex).Take(pageSize).ToList();


            var customersViewModel = new CustomersWithActiveReservationViewModel(
                customersWithActiveReservations, page);


            customersViewModel.CurrentPage = page;
            customersViewModel.TotalPages = totalPages;

            return customersViewModel;
        }

    }
}
 