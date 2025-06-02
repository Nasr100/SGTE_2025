import { RouteStopRequest, RouteStopResponse } from "./routeStop.dto";

export interface RouteRequest{
    name:string;
    description:string;
    isActive:boolean;
    routeStops?:Array<RouteStopRequest>
}

export interface RouteResponse{
    id:number;
    name:string;
    description:string;
    isActive:boolean;
    routeStops:Array<RouteStopResponse>;
}