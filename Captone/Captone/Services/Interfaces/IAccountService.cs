using System;
using System.Collections.Generic;
using System.Linq;
using Captone.Models;

namespace Captone.Services.Interfaces
{
    public interface IAccountService
    {
        List<Account> GetAllCustomers();
        List<Account> GetAllStaff();
        bool EditAccount(Account account, string role);
        Account GetAccount(string username);
    }
}