using Shared.Dtos;

namespace Route_Service.Reposetories.Route
{
    public interface IRouteRepo
    {
        public Task<RouteResponse> AddRoute(RouteRequest route);
        public IQueryable<Models.Route> GetAllRoutes();
        public  Task<Models.Route> GetRouteById(int id);
        public  Task DeleteRoute(int id);
        public Task<RouteResponse> UpdateRoute(int id, RouteRequest routeReq);
        public Task<RouteStopsResponse> AssignStop(RouteStopsRequest routeStopsRequest);
        public Task<List<RouteStopsResponse>?> GetAssingedStops(int RouteId);

    }
}
