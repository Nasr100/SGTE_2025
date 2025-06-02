import { Injectable } from '@angular/core';
import { GridifyBuilder } from '../../../shared/helpers/GridifyBuilder';
import { Observable } from 'rxjs';
import { makeRequestParams } from '../../../shared/helpers/GridifyRequestBuilder';
import { GridifyResponse } from '../../../shared/types/Dtos/gridify.dto';
import { RouteRequest, RouteResponse } from '../../../shared/types/Dtos/route.dto';
import { HttpClient, HttpParams } from '@angular/common/http';
import { RouteStopRequest } from '../../../shared/types/Dtos/routeStop.dto';
import { Params } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class RouteService {

  constructor(private client:HttpClient) { }
  url:string = `https://localhost:7025/api/route`;

  getRoutes(query:GridifyBuilder):Observable<GridifyResponse<RouteResponse>>{
      const params = makeRequestParams(query);
      return this.client.get<GridifyResponse<RouteResponse>>(this.url,{params}); 
    }

    getRouteById(id:number):Observable<RouteResponse>{
      return this.client.get<RouteResponse>(`${this.url}/${id}`);
    }
    // addStopToRoute(routeId:number,routeStopsRequest:RouteStopRequest):Observable<RouteResponse>{
    //   return this.client.post<RouteResponse>(`${this.url}`,{routeId,routeStopsRequest});
    // }

    updateRoute(routereq:RouteRequest,id:number):Observable<RouteResponse>{
      return this.client.put<RouteResponse>(`${this.url}/${id}`,{routereq});
    }
    deleteRoute(id:number){
      return this.client.delete<RouteResponse>(`${this.url}/${id}`);
    }
    assignStop(routeId:number,routeStopsRequest:RouteStopRequest):Observable<RouteStopRequest>{
      let params = new HttpParams().set("routeId",routeId);
      return this.client.post<RouteStopRequest>(`${this.url}/stop`,{params,routeStopsRequest});
    }

    deleteAssignedStop(routeId:number,stopId:number){
      let params = new HttpParams().set("routeId",routeId).set("stopId",stopId);
            return this.client.delete(`${this.url}/stop`,{params});
    }

    updateAssignedStop(routeId:number,routeStopsRequest:RouteStopRequest){
            let params = new HttpParams().set("routeId",routeId);
            return this.client.put(`${this.url}/stop`,{params});
    }




}
