using Mapster;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Route_Service.Data;
using Route_Service.Models;
using Shared.Dtos;

namespace Route_Service.Reposetories.Route
{
    public class RouteRepo : IRouteRepo
    {
        private readonly RouteServiceContext _context;
        private readonly ILogger<RouteRepo> _logger;
        public RouteRepo(RouteServiceContext context, ILogger<RouteRepo> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<RouteResponse> AddRoute(ComplexRouteStopsRequest route)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var routeModel = route.route.Adapt<Models.Route>();
                var x = await _context.Routes.AddAsync(routeModel);
                await _context.SaveChangesAsync();
                if (route.routeStops?.Count > 0)
                {

                    foreach (var stop in route.routeStops)
                    {
                        await this.AssignStop(x.Entity.Id, stop);
                    }

                }
                await transaction.CommitAsync();

                return routeModel.Adapt<RouteResponse>();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw;
            }
           
        }

        public  IQueryable<Models.Route> GetAllRoutes()
        {
            var routes =_context.Routes.Where(r => !r.IsDeleted).Include(r=>r.RouteStops).ThenInclude(rs=>rs.Stop);
            return routes;
        }

        public async Task<Models.Route> GetRouteById(int id)
        {
            var route = await _context.Routes.Where(r => !r.IsDeleted).Include(r=>r.RouteStops).ThenInclude(rs => rs.Stop).FirstAsync(r=>r.Id == id) ?? throw new Exception("route with id " + id + "not found");
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

        public async Task<RouteStopsResponse> AssignStop(int routeId, RouteStopsRequest routeStopsRequest)
        {
           
            var route = await GetRouteById(routeId);

            var stop = await _context.Stops.FindAsync(routeStopsRequest.StopId) ?? throw new ArgumentException("Stop not found");
            if (route.RouteStops.Any(rs=>rs.StopId == stop.Id))
            {
                throw new ArgumentException("Stop already assigned to this route");
            }
            var routeStops = routeStopsRequest.Adapt<RouteStops>();
            routeStops.RouteId = routeId;
            await _context.RouteStops.AddAsync(routeStops);
            await _context.SaveChangesAsync(); 
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

        public async Task<bool> IsCombinationUniqueAsync(RouteStopsRequest routeStopReq, int routeId, int? excludeId = null)
        {
            return !await _context.RouteStops
                .AnyAsync(rs =>
                    rs.StopId == routeStopReq.StopId &&
                    rs.RouteId == routeId &&
                    rs.StopOrder == routeStopReq.StopOrder &&
                    (excludeId == null));
        }



    }
}
