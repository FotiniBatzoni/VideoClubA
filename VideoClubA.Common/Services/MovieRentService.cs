using VideoClubA.Core.Interfaces;
using VideoClubA.Infrastucture.Data;
using VideoClubA.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace VideoClubA.Common.Services
{
    public class MovieRentService : IMovieRentService
    {
        private readonly VideoClubDbContext _context;

        public MovieRentService(VideoClubDbContext context)
        {
            _context = context;
        }

        public List<MovieRent> GetMovieRents()
        {
            //var activeReservations = (
            //    from mr in _context.MovieRents
            //    where mr.ReturnDate > DateTime.Now
            //    group mr by mr.CustomerId into g
            //    select new MovieRent
            //    {
            //        CustomerId = g.Key,
            //        ActiveReservations = g.Count()
            //    }
            //).ToList();

            return _context.MovieRents.
                Where(mr => mr.ReturnDate > DateTime.Now)
                .AsNoTracking()
                .ToList();

        }
    }
}




