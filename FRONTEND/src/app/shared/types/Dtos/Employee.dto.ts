import { GroupResponse } from "./group.dto";
import { StopResponse } from "./stop.dto";

export interface EmployeeRequest{
     firstName:string;
     lastName:string;
     phoneNumber:string;
     badgeNumber:string;
     role:string;
     email:string;
     password:string;
     address:string;
     status:string;
     stopId:number;
     groupId:number;

} 
export interface EmployeeResponse{
     id:number;
     firstName:string;
     lastName:string;
     phoneNumber:string;
     badgeNumber:string;
     role:string;
     email:string;
     password:string;
     address:string;
     status:string;
     stop:StopResponse;
     group:GroupResponse;


} 