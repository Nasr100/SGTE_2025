import { Pagination } from "../../helpers/pagination";

export interface GridifyResponse<T>{
    count:number;
    data:T[];
}



export interface GridifyRequest{
    filters?:string;
    search?:string;
    sort?:string;
    pagination:Pagination;
}