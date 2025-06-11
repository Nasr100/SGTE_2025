using GeoCoordinatePortable;
using Google.OrTools.ConstraintSolver;
using static Google.Protobuf.Reflection.SourceCodeInfo.Types;
using System.Collections.Generic;
using System.Linq;
using Route_Service.Models;

namespace Route_Service.Services.RouetCalculator
{
    //public class RouteCalculatorService
    //{
    //    public RouteSolution SolveRouteWithTimeConstraint(
    //     List<Models.Location> locations,
    //     int vehicleSpeedKph,
    //     int maxRouteMinutes = 50)
    //    {
    //        // Convert to distance matrix (in meters)
    //        var distanceMatrix = CreateDistanceMatrix(locations);

    //        // Create routing model
    //        var routing = new RoutingModel(
    //            locations.Count, // Number of locations
    //            1,              // Number of vehicles
    //            0               // Depot index (starting point)
    //        );

    //        // Define distance callback
    //        var transitCallbackIndex = routing.RegisterTransitCallback(
    //            (fromIndex, toIndex) =>
    //            {
    //                var fromNode = routing.IndexToNode(fromIndex);
    //                var toNode = routing.IndexToNode(toIndex);
    //                return distanceMatrix[fromNode, toNode];
    //            });

    //        routing.SetArcCostEvaluatorOfAllVehicles(transitCallbackIndex);

    //        // Add time dimension (convert meters to minutes based on speed)
    //        var speedMetersPerMinute = (vehicleSpeedKph * 1000) / 60.0;
    //        routing.AddDimension(
    //            transitCallbackIndex,
    //            slack_max: maxRouteMinutes,  // Maximum wait time at nodes
    //            capacity: maxRouteMinutes,  // Maximum route duration
    //            fix_start_cumul_to_zero: true,
    //            name: "Time");

    //        var timeDimension = routing.GetDimensionOrDie("Time");

    //        // Solve
    //        var searchParameters = operations_research_constraint_solver.DefaultRoutingSearchParameters();
    //        searchParameters.FirstSolutionStrategy = FirstSolutionStrategy.Types.Value.PathCheapestArc;

    //        var solution = routing.SolveWithParameters(searchParameters);

    //        // Extract solution
    //        return ExtractSolution(routing, solution, timeDimension, locations);
    //    }


    //    private long[,] CreateDistanceMatrix(List<Location> locations)
    //    {
    //        int count = locations.Count;
    //        var matrix = new long[count, count];

    //        for (int i = 0; i < count; i++)
    //        {
    //            for (int j = 0; j < count; j++)
    //            {
    //                matrix[i, j] = (long)locations[i].DistanceTo(locations[j]);
    //            }
    //        }

    //        return matrix;
    //    }

    //    private RouteSolution ExtractSolution(
    //   RoutingModel routing,
    //   Assignment solution,
    //   RoutingDimension timeDimension,
    //   List<Location> locations)
    //    {
    //        var routeSolution = new RouteSolution();
    //        long routeDuration = 0;

    //        // Start from the depot (index 0)
    //        long index = routing.Start(0);

    //        while (!routing.IsEnd(index))
    //        {
    //            var nodeIndex = routing.IndexToNode(index);
    //            routeSolution.OrderedLocations.Add(locations[nodeIndex]);

    //            // Get cumulative time at this node
    //            var timeVar = timeDimension.CumulVar(index);
    //            routeSolution.NodeArrivalTimes.Add(solution.Value(timeVar));

    //            index = solution.Value(routing.NextVar(index));
    //        }

    //        // Add final node time
    //        var endTimeVar = timeDimension.CumulVar(index);
    //        routeDuration = solution.Value(endTimeVar);
    //        routeSolution.TotalDurationMinutes = routeDuration;

    //        return routeSolution;
    //    }
    //}

}

