using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Captone.Models;

namespace Captone.Repositories.Interfaces
{
    public class InvoiceRepository : BaseRepository, IInvoiceRepository
    {
        public InvoiceRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public IQueryable<Invoice> GetAllInvoices()
        {
            return GetDbSet<Invoice>();
        }

        public void Edit(Invoice invoice)
        {
            Update(invoice);
        } 
    }
}