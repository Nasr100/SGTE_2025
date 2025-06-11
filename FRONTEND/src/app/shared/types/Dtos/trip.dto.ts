
export interface TripRequest{
    name:string;
    direction:string;
    type:string;
    shift:string;
}


export interface TripResponse{
    id:number;
    name:string;
    direction:string;
    type:string;
    shift:string;
}