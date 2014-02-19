using System.Linq;
using System.Collections.Generic;
using Captone.Extensions;
using Captone.Models;
using Captone.Repositories.Interfaces;
using Captone.Services.Interfaces;

namespace Captone.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public List<Account> GetAllCustomers()
        {
            return _accountRepository.GetAllAccounts().Where(i => i.Role == Resource.Customer).ToList();
        }

        public List<Account> GetAllStaff()
        {
            return _accountRepository.GetAllAccounts().Where(i => i.Role == Resource.Staff).ToList();
        }

        public bool EditAccount(Account account, string role)
        {
            var hasPrivileage = false;
            switch (role)
            {
                case Constants.Admin:
                    hasPrivileage = true;
                    break;
                case Constants.Staff:
                    hasPrivileage = account.Role == Resource.Customer;
                    break;
                case Constants.Customer:
                    break;
            }
            if (hasPrivileage)
            {
                _accountRepository.Edit(account);
            }
            return true;
        }

        public Account GetAccount(string username)
        {
            return _accountRepository.GetAllAccounts().SingleOrDefault(i => i.Username == username);
        }
    }
}