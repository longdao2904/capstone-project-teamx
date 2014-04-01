using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Captone.Models;
using Captone.Repositories;
using Captone.Repositories.Interfaces;
using Captone.Services.Interfaces;

namespace Captone.Services
{
    public class ManageFeeService: IManageFeeService
    {
        private readonly IManageFeeRepository _manageFeeRepository;

        public ManageFeeService(IManageFeeRepository manageFeeRepository)
        {
            _manageFeeRepository = manageFeeRepository;
        }

        public List<ManageFee> GetAllManageFees()
        {
            return _manageFeeRepository.GetAllManageFees().ToList();
        }
    }
}