import { GroupResponse } from "../../../shared/types/Dtos/group.dto";
import { StopResponse } from "../../../shared/types/Dtos/stop.dto";

export class Employee{
      public id:number;
    public firstName:string;
    public lastName:string;
    public phoneNumber:string;
    public badgeNumber:string;
    public role:string;
    public email:string;
    public password:string;
    public address:string;
    public status:string;
    public stop:StopResponse;
    public group:GroupResponse;
    public isChecked:boolean;

     constructor( id:number,firstName:string, lastName:string,phoneNumber:string,badgeNumber:string,role:string,email:string,password:string,address:string,status:string,stop:StopResponse,group:GroupResponse){
        this.address = address;
        this.badgeNumber = badgeNumber;
        this.email = email;
        this.firstName = firstName;
        this.group = group;
        this.id = id;
        this.lastName = lastName;
        this.password =password;
        this.phoneNumber = phoneNumber;
        this.role = role;
        this.status = status;
        this.stop = stop;
        this.isChecked = false;
     }

     toggleIsChecked(){
        this.isChecked =!this.isChecked;
     }
    

}