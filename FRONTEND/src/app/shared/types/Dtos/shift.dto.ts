export interface ShiftRequest{
    shift:string;
    role:string;
    startShift:string;
    endShift:string;
}

export interface ShiftResponse{
    id:number;
   shift:string;
    role:string;
    startShift:string;
    endShift:string;
    
}