using Gridify;
using Shared.Dtos;

namespace Route_Service.Services.Route
{
    public interface IRouteService
    {
        public  Task<RouteResponse> AddRoute(RouteRequest route);
        public Paging<RouteResponse> GetRoutes(GridifyQuery gridifyQuery);
        public  Task<RouteResponse> GetRouteById(int id);
        public  Task Delete(int id);
        public Task<RouteResponse> UpdateRoute(int id, RouteRequest routeRequest);


    }
}
