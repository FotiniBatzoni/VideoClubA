using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoClubA.Core.Entities;
using VideoClubA.Core.Interfaces;
using VideoClubA.Infrastucture.Data;

namespace VideoClubA.Common.Services
{
    public class MovieCopyService : IMovieCopyService
    {
        private readonly VideoClubDbContext _context;

        public MovieCopyService(VideoClubDbContext context)
        {
            _context = context;
        }
        public List<MovieCopy> GetAllAvailabiliy()
        {
            return (from moviecopies in _context.MovieCopies
                    where moviecopies.IsAvailable == true
                    select moviecopies).ToList();
        }
    }
}
