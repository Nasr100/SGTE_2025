import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { GridifyBuilder } from '../../../shared/helpers/GridifyBuilder';
import { makeRequestParams } from '../../../shared/helpers/GridifyRequestBuilder';
import { GridifyResponse } from '../../../shared/types/Dtos/gridify.dto';
import { TripRequest, TripResponse } from '../../../shared/types/Dtos/trip.dto';

@Injectable({
  providedIn: 'root'
})
export class TripService {
  url:string = `https://localhost:7025/api/route`;

  constructor(private client:HttpClient) { }

  getTrips(query:GridifyBuilder):Observable<GridifyResponse<TripResponse>>{
      const params = makeRequestParams(query);
      return this.client.get<GridifyResponse<TripResponse>>(this.url,{params}); 
    }

    getTripById(id:number):Observable<TripResponse>{
      return this.client.get<TripResponse>(`${this.url}/${id}`)
    }

    addTrip(trip:TripRequest):Observable<TripResponse>{
      return this.client.post<TripResponse>(`${this.url}`,{trip});
    }
    
    deleteTrip(id:number){
       return this.client.delete(`${this.url}/${id}`)
    }
}
