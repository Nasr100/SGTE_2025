using Shared.Dtos;

namespace Route_Service.Reposetories.Route
{
    public interface IRouteRepo
    {
        public Task<RouteResponse> AddRoute(ComplexRouteStopsRequest route);
        public IQueryable<Models.Route> GetAllRoutes();
        public  Task<Models.Route> GetRouteById(int id);
        public  Task DeleteRoute(int id);
        public Task<RouteResponse> UpdateRoute(int id, RouteRequest routeReq);
        public Task<RouteStopsResponse> AssignStop(int routeId, RouteStopsRequest routeStopsRequest);
        public Task<List<RouteStopsResponse>?> GetAssingedStops(int RouteId);
        public Task DeleteAssignedStop(int routeId, int stopId);
        public Task<RouteStopsResponse> UpdateAssignedRoute(RouteStopsRequest routeStopReq, int routeId, int stopId);
        public Task<bool> IsCombinationUniqueAsync(RouteStopsRequest routeStopReq, int routeId, int? excludeId = null);

    }
}
