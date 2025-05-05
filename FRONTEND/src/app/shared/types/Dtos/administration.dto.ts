import { EmployeeRequest, EmployeeResponse } from "./Employee.dto";

export interface AdministartionRequest{
    departement:string;
    employee:EmployeeRequest;
}


export interface AdministartionResponse{
    id:number;
    departement:string;
    employee:EmployeeResponse;
}