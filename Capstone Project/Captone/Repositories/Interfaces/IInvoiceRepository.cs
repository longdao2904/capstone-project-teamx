using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Captone.Models;

namespace Captone.Repositories.Interfaces
{
    public interface IInvoiceRepository
    {
        IQueryable<Invoice> GetAllInvoices();
        void Edit(Invoice invoice);
    }
}