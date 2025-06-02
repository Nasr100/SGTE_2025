using Gridify;
using Mapster;
using Route_Service.Reposetories.Route;
using Shared.Dtos;

namespace Route_Service.Services.Route
{
    public class RouteService : IRouteService
    {
        private readonly IRouteRepo _routeRepo;

        public RouteService(IRouteRepo routeRepo)
        {
            _routeRepo = routeRepo;
        }

        public async Task<RouteResponse> AddRoute(ComplexRouteStopsRequest route)
        {
            var response = await _routeRepo.AddRoute(route);
            return response;
        }

        public  Paging<RouteResponse> GetRoutes(GridifyQuery gridifyQuery)
        {
            var routes = _routeRepo.GetAllRoutes().Gridify(gridifyQuery);
            return routes.Adapt<Paging<RouteResponse>>();
        }

        public async Task<RouteResponse> GetRouteById(int id)
        {
            var route = await _routeRepo.GetRouteById(id);
            return route.Adapt<RouteResponse>();
        }

        public async Task DeleteRoute(int id)
        {
            await _routeRepo.DeleteRoute(id);
        }

        public async Task<RouteResponse> UpdateRoute(int id,RouteRequest routeRequest)
        {
            var route = await _routeRepo.UpdateRoute(id, routeRequest);
            return route;
        }

        public async Task<RouteStopsResponse> AssignStop(int routeId, RouteStopsRequest routeStopsRequest)
        {
            var isUnique = await _routeRepo.IsCombinationUniqueAsync(routeStopsRequest, routeId);
            if (!isUnique  )
            {
                throw new InvalidOperationException("This stop-route-order combination already exists\"");

            }

            if (routeStopsRequest.ArrivalTime >= routeStopsRequest.DepartureTime)
            {
                throw new InvalidOperationException("Arrival time must be before departure time");

            }

            var routeStop = await _routeRepo.AssignStop(routeId, routeStopsRequest);
            return routeStop;
        }

        public async Task DeleteAssignedStop(int routeId,int StopId)
        {
            await _routeRepo.DeleteAssignedStop(routeId,StopId);
            
        }

        public async Task<RouteStopsResponse> UpdateAssignedstop(RouteStopsRequest routeStopReq, int routeId, int stopId)
        {
            var routeStop = await _routeRepo.UpdateAssignedRoute(routeStopReq, routeId, stopId);
            return routeStop;
        }
    }
}
