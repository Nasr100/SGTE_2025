
export interface StopRequest{
    name:string;
    address:string;
    x:number;
    description:string;
    y:number;
    status:string;
}


export interface StopResponse{
    id:number;
    name:string;
    address:string;
    description:string;
    x:number;
    y:number;
    point:number[];
    status:string;
}