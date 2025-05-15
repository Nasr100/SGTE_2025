import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { GridifyRequest, GridifyResponse } from '../../../shared/types/Dtos/gridify.dto';
import { Observable } from 'rxjs';
import { BusRequest, BusResponse } from '../../../shared/types/Dtos/bus.dto';
import { makeEmployeeRequest } from '../../../shared/helpers/GridifyRequestBuilder';
import { env } from '../../../../environments/environment.dev';

@Injectable({
  providedIn: 'root'
})
export class BusService {

  constructor(private client:HttpClient) { }
  url:string = `${env.apiBaseUrl}/routes/bus`;
  getBuses(query:GridifyRequest):Observable<GridifyResponse<BusResponse>>{
          const params = makeEmployeeRequest(query,[{field:"employee.badgeNumber",type:"string"},{field:"employee.firstname",type:"string"},{field:"employee.lastname",type:"string"}]);
          return this.client.get<GridifyResponse<BusResponse>>(this.url,{params})
  }

  addBus(bus:BusRequest):Observable<BusResponse>{
    return this.client.post<BusResponse>(this.url,bus);
  }

  getBusById(id:number,bus:BusRequest):Observable<BusResponse>{
    return this.client.get<BusResponse>(`${this.url}/${id}`)
  }

  deleteBus(id:number){
    return this.client.delete(`${this.url}/${id}`);
  }

  updateBus(id:number,bus:BusRequest){
    return this.client.put(`${this.url}/${id}`,bus);
  }
}
