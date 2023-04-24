using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;
using VideoClubA.Core.Entities;
using VideoClubA.Core.Enumerations;
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
        public IActionResult CustomerPanel()
        {
            List<Customer> customers = _customerDb.GetAllCustomers();

            Dictionary<string, int> availabilityPerMovie = GetActiveReservationsPerCustomer();

            var custumersWithActiveReservations = _mapper.Map<List<CustomerActiveReservationsViewModel>>
                (customers, opt => opt.Items["ActiveReservations"] = availabilityPerMovie);


            return View(custumersWithActiveReservations);
        }

        private Dictionary<string, int> GetActiveReservationsPerCustomer()
        {
            List<MovieRent> movieRents = _movieRentDb.GetMovieRents();

            var activeReservationsByCustomer = movieRents
                .GroupBy(mr => mr.CustomerId)
                .ToDictionary(g => g.Key, g => g.Count());

            return activeReservationsByCustomer;
        }

    }
}
 