import { Employee } from "../../../shared/types/models/employee.model";

export interface Administrations{
    id:number;
    departement:string;
    employee:Employee;
    ischecked:boolean;
} 