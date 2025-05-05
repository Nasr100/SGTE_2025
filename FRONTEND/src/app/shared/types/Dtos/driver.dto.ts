import { permisType } from "../../../features/Drivers/types/driver.model";
import { EmployeeRequest } from "./Employee.dto";

export interface DriverRequest{
    employee:EmployeeRequest;
    licenceNumber:string;
    permis:permisType;
}


export interface DriverResponse{
    id:number;
    employee:EmployeeRequest;
    licenceNumber:string;
    permis:permisType;
}