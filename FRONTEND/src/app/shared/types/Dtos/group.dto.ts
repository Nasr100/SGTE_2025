import { ShiftResponse } from "./shift.dto";

export interface GroupRequest{
    name:string;
    role:string;
    shiftId:number;
}

export interface GroupResponse{
    id:number;
    name:string;
    role:string;
    shift:ShiftResponse;
}