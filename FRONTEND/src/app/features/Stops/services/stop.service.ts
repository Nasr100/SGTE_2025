import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { GridifyRequest, GridifyResponse } from '../../../shared/types/Dtos/gridify.dto';
import { StopRequest, StopResponse } from '../../../shared/types/Dtos/stop.dto';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import {  makeRequestParams } from '../../../shared/helpers/GridifyRequestBuilder';
import { env } from '../../../../environments/environment.dev';
import { GridifyBuilder } from '../../../shared/helpers/GridifyBuilder';

@Injectable({
  providedIn: 'root'
})
export class StopService {
  

  constructor(private client:HttpClient) { }
    // url:string = `${env.apiBaseUrl}/routes/stop`;
    url:string = `https://localhost:7025/api/route/Stop`;


  getStops(query:GridifyBuilder):Observable<GridifyResponse<StopResponse>>{
    const params = makeRequestParams(query);
    return this.client.get<GridifyResponse<StopResponse>>(this.url,{params}); 
  }

  addStop(stop:StopRequest):Observable<StopResponse>{
    return this.client.post<StopResponse>(this.url,stop);
  }

  getStopById(id:number):Observable<StopResponse>{
    return this.client.get<StopResponse>(`${this.url}/${id}`);
  }

  deleteStop(id:number){
    return this.client.delete(`${this.url}/${id}`);
  }

  updateStop(stopReq:StopRequest,id:number){
    return this.client.put(`${this.url}/${id}`,stopReq);
  }

}
