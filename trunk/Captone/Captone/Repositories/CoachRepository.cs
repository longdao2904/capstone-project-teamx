using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Captone.Models;
using Captone.Repositories.Interfaces;

namespace Captone.Repositories
{
    public class CoachRepository : BaseRepository, ICoachRepository
    {
        public CoachRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public IQueryable<Coach> GetAllCoaches()
        {
            return GetDbSet<Coach>();
        }
    }
}