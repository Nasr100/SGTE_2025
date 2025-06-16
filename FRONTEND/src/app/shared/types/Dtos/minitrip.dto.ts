import { BusResponse } from "./bus.dto";
import { EmployeeResponse } from "./Employee.dto";

export interface MinitripRequest{
    tripId:number;
    name:string;
    busId:number;
    driverId:number;
}
export interface MinitripResponse{
    id:number;
    name:string;
    // trip:trip;
    bus:BusResponse;
    driver:EmployeeResponse;
}