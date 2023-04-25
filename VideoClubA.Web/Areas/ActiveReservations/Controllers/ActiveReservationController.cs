using Microsoft.AspNetCore.Mvc;

namespace VideoClubA.Web.Areas.ActiveReservations.Controllers
{
    public class ActiveReservationController : Controller
    {
        [HttpGet]
        [Area("ActiveReservations")]
        public IActionResult ActiveReservationsPanel()
        {
            return View();
        }
    }
}
//https://localhost:7210/ActiveReservation/ActiveReservationsPanel?area=ActiveReservations

//https://localhost:7210/Customers/Customer/CustomerPanel