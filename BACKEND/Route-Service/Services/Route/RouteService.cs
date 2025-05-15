using Gridify;
using Mapster;
using Route_Service.Reposetories.Route;
using Shared.Dtos;

namespace Route_Service.Services.Route
{
    public class RouteService
    {
        private readonly RouteRepo _routeRepo;

        public RouteService(RouteRepo routeRepo)
        {
            _routeRepo = routeRepo;
        }

        public async Task<RouteResponse> AddRoute(RouteRequest route)
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

        public async Task Delete(int id)
        {
            await _routeRepo.DeleteRoute(id);
        }

        public async Task<RouteResponse> UpdateRoute(int id,RouteRequest routeRequest)
        {
            var route = await _routeRepo.UpdateRoute(id, routeRequest);
            return route;
        }
    }
}
