using Mapster;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Route_Service.Data;
using Route_Service.Models;
using Shared.Dtos;

namespace Route_Service.Reposetories.Route
{
    public class RouteRepo : IRouteRepo
    {
        private RouteServiceContext _context;
        public RouteRepo(RouteServiceContext context)
        {
            _context = context;
        }

        public async Task<RouteResponse> AddRoute(RouteRequest route)
        {
            var routeModel = route.Adapt<Models.Route>();
            await _context.Routes.AddAsync(routeModel);
            await _context.SaveChangesAsync();
            return routeModel.Adapt<RouteResponse>();
        }

        public  IQueryable<Models.Route> GetAllRoutes()
        {
            var routes =_context.Routes.Where(r=>r.IsDeleted == false);
            return routes;
        }

        public async Task<Models.Route> GetRouteById(int id)
        {
            var route = await _context.Routes.Include(r=>r.Stops).FirstAsync(r=>r.Id == id) ?? throw new Exception("route with id " + id + "not found");
            return route;
        }

        public async Task DeleteRoute(int id)
        {
            var route = await GetRouteById(id);
            route.IsDeleted = true;
            _context.Routes.Update(route);  
            await _context.SaveChangesAsync();
        }

        public async Task<RouteResponse> UpdateRoute(int id,RouteRequest routeReq)
        {
            var route = await GetRouteById(id);
            routeReq.Adapt(route);
            await _context.SaveChangesAsync();
            return route.Adapt<RouteResponse>();
        }

        public async Task<RouteStopsResponse> AssignStop(RouteStopsRequest routeStopsRequest)
        {
            var route = await GetRouteById(routeStopsRequest.RouteId);
            var stop = await _context.Stops.FindAsync(routeStopsRequest.StopId) ?? throw new ArgumentException("Stop not found");
            if (route.RouteStops.Any(rs=>rs.StopId == stop.Id))
            {
                throw new ArgumentException("Stop already assigned to this route");
            }
            var routeStops = routeStopsRequest.Adapt<RouteStops>();
            await _context.RouteStops.AddAsync(routeStops);
            return routeStops.Adapt<RouteStopsResponse>();
        }

        public async Task<List<RouteStopsResponse>?> GetAssingedStops(int RouteId)
        {
            var stops = await _context.RouteStops.Where(rs => rs.RouteId == RouteId).ToListAsync();
            return stops.Adapt<List<RouteStopsResponse>>();
        }

        public async Task DeleteAssignedStop(int routeId,int stopId)
        {
            var routeStop = await _context.RouteStops.FirstOrDefaultAsync(rs => rs.RouteId == routeId && rs.StopId == stopId);
            if (routeStop != null)
            {
                _context.RouteStops.Remove(routeStop);
                await _context.SaveChangesAsync();
            }
        } 
        
        public async Task<RouteStopsResponse> UpdateAssignedRoute(RouteStopsRequest routeStopReq,int routeId,int stopId)
        {
            var routeStop = await _context.RouteStops.FirstOrDefaultAsync(rs => rs.RouteId == routeId && rs.StopId == stopId) ?? throw new ArgumentException("Stop not found");
            routeStopReq.Adapt(routeStop);
            await _context.SaveChangesAsync();
            return routeStopReq.Adapt<RouteStopsResponse>();

        }



    }
}
