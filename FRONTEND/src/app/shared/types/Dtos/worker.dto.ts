import { EmployeeRequest, EmployeeResponse } from "./Employee.dto"

export interface WorkerRequest{
    employee:EmployeeRequest
}

export interface WorkerResponse{
    id:number
    employee:EmployeeResponse
    
}