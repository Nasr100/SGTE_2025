import { Employee } from "../models/employee.model";

export interface AdministartionRequest{
    departement:string;
    employee:Employee;
}


export interface AdministartionResponse{
    id:number;
    departement:string;
    employee:Employee;
}