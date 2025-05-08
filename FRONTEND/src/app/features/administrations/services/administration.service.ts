import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { env } from '../../../../environments/environment.dev';
import { GridifyRequest, GridifyResponse } from '../../../shared/types/Dtos/gridify.dto';
import { Observable } from 'rxjs';
import { AdministartionRequest, AdministartionResponse } from '../../../shared/types/Dtos/administration.dto';
import { makeEmployeeRequest } from '../../../shared/helpers/GridifyRequestBuilder';

@Injectable({
  providedIn: 'root'
})
export class AdministrationService {

  constructor(private client:HttpClient) { }
   
  url:string = `${env.apiBaseUrl}/users/administration`;

  getAdministrations(query:GridifyRequest):Observable<GridifyResponse<AdministartionResponse>>{
    const params = makeEmployeeRequest(query,[{field:"employee.badgeNumber",type:"string"},{field:"employee.firstname",type:"string"},{field:"employee.lastname",type:"string"}]);
    return this.client.get<GridifyResponse<AdministartionResponse>>(this.url,{params});
  }

  addAdministration(administrationReq:AdministartionRequest):Observable<AdministartionResponse>{
    return this.client.post<AdministartionResponse>(this.url,administrationReq);
  }

  updateAdministration(id:number,administrationReq:AdministartionRequest){
    return this.client.put(`${this.url}/${id}`,{administrationReq})
  }

  deleteAdministration(id:number){
    return this.client.delete(`${this.url}/${id}`)
  }

  getAdministrationById(id:number):Observable<AdministartionResponse>{
    return this.client.get<AdministartionResponse>(`${this.url}/${id}`);
  }
}
