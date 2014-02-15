using System.Collections.Generic;
using Captone.Models;

namespace Captone.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        List<Account> GetAllAccounts();
    }
}