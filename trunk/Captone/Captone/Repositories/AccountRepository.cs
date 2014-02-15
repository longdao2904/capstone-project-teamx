using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Providers.Entities;
using Captone.Models;
using Captone.Repositories.Interfaces;

namespace Captone.Repositories
{
    public class AccountRepository : BaseRepository, IAccountRepository
    {
        public AccountRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public List<Account> GetAllAccounts()
        {
            return GetDbSet<Account>().ToList();
        }
    }
}