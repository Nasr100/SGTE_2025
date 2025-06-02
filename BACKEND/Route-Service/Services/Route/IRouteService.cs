using Gridify;
using Shared.Dtos;

namespace Route_Service.Services.Route
{
    public interface IRouteService
    {
        public  Task<RouteResponse> AddRoute(ComplexRouteStopsRequest route);
        public Paging<RouteResponse> GetRoutes(GridifyQuery gridifyQuery);
        public  Task<RouteResponse> GetRouteById(int id);
        public  Task DeleteRoute(int id);
        public Task<RouteResponse> UpdateRoute(int id, RouteRequest routeRequest);
        public  Task<RouteStopsResponse> AssignStop(int routeId, RouteStopsRequest routeStopsRequest);
        public Task DeleteAssignedStop(int routeId, int StopId);
        public Task<RouteStopsResponse> UpdateAssignedstop(RouteStopsRequest routeStopReq, int routeId, int stopId);

    }
}
