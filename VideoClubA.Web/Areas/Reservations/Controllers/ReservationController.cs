using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VideoClubA.Core.Entities;
using VideoClubA.Core.Interfaces;
using VideoClubA.Web.Areas.Reservations.Models;

namespace VideoClubA.Web.Areas.Reservations.Controllers
{
 
    public class ReservationController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMovieRentService _rentsDb;

        public ReservationController(IMapper mapper, IMovieRentService rentsDb)
        {
            _rentsDb = rentsDb;
            _mapper = mapper;
        }

        [HttpGet]
        [Area("Reservations")]
        public IActionResult ReservationsPerCustomer( string customerId, string firstName, string lastName, 
            int page = 1, int pageSize = 5)
        {
            return View(PaginateRents(page, pageSize,customerId, firstName,lastName));
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
    }
}
