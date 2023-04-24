using AutoMapper;
using VideoClubA.Core.Entities;
using VideoClubA.Web.Areas.Customers.Models;

namespace VideoClubA.Web.Profiler
{
    public class CustomerWithActiveReservationsProfiler : Profile
    {
        public CustomerWithActiveReservationsProfiler()
        {
            CreateMap<MovieRent, CustomerActiveReservationsViewModel>();
            CreateMap<Customer, CustomerActiveReservationsViewModel>()
               .ForMember(dest => dest.ActiveReservations, opt => opt.MapFrom<CustomerWithActiveReservationsResolver>());

        }
    }
}