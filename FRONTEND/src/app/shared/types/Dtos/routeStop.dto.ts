import { StopResponse } from "./stop.dto";

export interface RouteStopRequest{
    stopId:number;
    stopOrder:number;
    arrivalTime:string;
    departureTime:string;
}
export interface RouteStopResponse{
    stop:StopResponse;
    stopOrder:number;
    arrivalTime:string;
    departureTime:string;
}