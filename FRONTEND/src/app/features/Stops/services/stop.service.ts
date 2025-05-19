import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { GridifyRequest, GridifyResponse } from '../../../shared/types/Dtos/gridify.dto';
import { StopRequest, StopResponse } from '../../../shared/types/Dtos/stop.dto';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { makeEmployeeRequest } from '../../../shared/helpers/GridifyRequestBuilder';
import { env } from '../../../../environments/environment.dev';

@Injectable({
  providedIn: 'root'
})
export class StopService {
  

  constructor(private client:HttpClient) { }
    // url:string = `${env.apiBaseUrl}/routes/stop`;
    url:string = `https://localhost:7025/api/route/Stop`;

  getStops(query:GridifyRequest):Observable<GridifyResponse<StopResponse>>{
    const params = makeEmployeeRequest(query,[{field:"employee.badgeNumber",type:"string"},{field:"employee.firstname",type:"string"},{field:"employee.lastname",type:"string"}]);
    return this.client.get<GridifyResponse<StopResponse>>(this.url,{params}); 
  }

  addStop(stop:StopRequest):Observable<StopResponse>{
    return this.client.post<StopResponse>(this.url,stop);
  }

  getStopById(id:number):Observable<StopResponse>{
    return this.client.get<StopResponse>(`${this.url}/${id}`);
  }


}
