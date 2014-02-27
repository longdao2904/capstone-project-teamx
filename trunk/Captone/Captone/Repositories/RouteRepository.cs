using Captone.Models;
using Captone.Repositories.Interfaces;
using System.Linq;

namespace Captone.Repositories
{
    public class RouteRepository : BaseRepository, IRouteRepository
    {
        public RouteRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public IQueryable<Route> GetAllRoutes()
        {
            return base.GetAll<Route>();
        }
    }
}