using AutoMapper;
using System.Linq;
using VideoClubA.Core.Entities;
using VideoClubA.Web.Areas.Customers.Models;
using VideoClubA.Web.Areas.Movies.Models;

namespace VideoClubA.Web.Profiler
{
    public class CustomerWithActiveReservationsResolver : IValueResolver<Customer, CustomerActiveReservationsViewModel, object>
    {
        private Dictionary<string, int> _activeReservations;

        public CustomerWithActiveReservationsResolver(Dictionary<string, int> activeReservations)
        {
            _activeReservations = activeReservations;
        }

   
        //public object Resolve(Customer source, CustomerActiveReservationsViewModel destination, int destMember, ResolutionContext context)
        //{
        //    if (context.Items.TryGetValue("ActiveReservations", out object activeReservationsDictObj))
        //    {
        //        Dictionary<string, int> activeReservationsDict = (Dictionary<string, int>)activeReservationsDictObj;
        //        if (activeReservationsDict.ContainsKey(source.Id))
        //        {
        //            return activeReservationsDict[source.Id];
        //        }
        //    }
        //    return 0;
        //}

        public object Resolve(Customer source, CustomerActiveReservationsViewModel destination, object destMember, ResolutionContext context)
        {
            if (context.Items.TryGetValue("ActiveReservations", out object activeReservationsDictObj))
            {
                Dictionary<string, int> activeReservationsDict = (Dictionary<string, int>)activeReservationsDictObj;
                if (activeReservationsDict.ContainsKey(source.Id))
                {
                    return activeReservationsDict[source.Id];
                }
            }
            return 0;
        }
    }
}
