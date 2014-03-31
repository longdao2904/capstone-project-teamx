using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Captone.Models;
using Captone.Repositories.Interfaces;

namespace Captone.Repositories
{
    public class ManageFeeRepository : BaseRepository, IManageFeeRepository
    {
        public ManageFeeRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public IQueryable<ManageFee> GetAllManageFees()
        {
            return GetDbSet<ManageFee>();
        }

        public void Edit(ManageFee manageFee)
        {
            Update(manageFee);
        } 
    }
}