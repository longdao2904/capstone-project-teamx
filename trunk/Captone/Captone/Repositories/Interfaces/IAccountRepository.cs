using System.Collections.Generic;
using System.Linq;
using Captone.Models;

namespace Captone.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        IQueryable<Account> GetAllAccounts();
        void Edit(Account account);
    }
}

