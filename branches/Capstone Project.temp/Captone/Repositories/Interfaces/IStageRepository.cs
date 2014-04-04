using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Captone.Models;

namespace Captone.Repositories.Interfaces
{
    public interface IStageRepository
    {
        IQueryable<Stage> GetAllAccounts();
        void Edit(Stage stage);
    }
}