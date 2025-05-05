import { Employee } from "../../../shared/types/models/employee.model";

export interface Driver{
    employee:Employee;
    id:number;
    licenceNumber:string;
    permis:permisType;

}

export enum permisType{
    A,B,C,D
}