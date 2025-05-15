export interface OpenRouteServiceRequest{
    text:string;
    country:string;
    // point:{lon:number,lat:number}
}
export interface OpenRouteServiceResponse{
    features:[{geometry:{coordinates:[number,number]}},{properties:{name:string,street:string}}];
}