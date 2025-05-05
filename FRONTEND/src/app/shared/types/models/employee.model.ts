import { Role } from "./Role.model";

export interface Employee{
    id:number;
     firstName:string;
     lastName:string;
     phoneNumber:string;
     badgeNumber:string;
    //  roles:Array<Role>;
     email:string;
     password:string;
     address:string;
     isAdmin:boolean;

} 