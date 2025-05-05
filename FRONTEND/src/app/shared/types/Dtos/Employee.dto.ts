import { Role } from "../models/Role.model";

export interface EmployeeRequest{
     firstName:string;
     lastName:string;
     phoneNumber:string;
     badgeNumber:string;
    //  roles:Array<Role>;
     email:string;
     password:string;
     address:string
     isAdmin:boolean;

} 
export interface EmployeeResponse{
    id:number;
     firstName:string;
     lastName:string;
     phoneNumber:string;
     badgeNumber:string;
    //  roles:Array<Role>;
     email:string;
     password:string;
     address:string
     isAdmin:boolean;

} 