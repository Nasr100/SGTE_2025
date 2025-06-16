import { Injectable } from '@angular/core';
import { GroupResponse } from '../../../shared/types/Dtos/group.dto';
import { GridifyResponse } from '../../../shared/types/Dtos/gridify.dto';
import { makeRequestParams } from '../../../shared/helpers/GridifyRequestBuilder';
import { HttpClient, HttpParams } from '@angular/common/http';
import { GridifyBuilder } from '../../../shared/helpers/GridifyBuilder';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class GroupService {
  baseUrl:string="";
  constructor(private client:HttpClient) { }

  getGroups(query:GridifyBuilder):Observable<GridifyResponse<GroupResponse>>{
        const params = makeRequestParams(query);
        return this.client.get<GridifyResponse<GroupResponse>>(this.baseUrl,{params}); 
      }
  getGroupByRole(role:string):Observable<GridifyResponse<GroupResponse>>{
     const params = new HttpParams().set('role', role);
        return this.client.get<GridifyResponse<GroupResponse>>(this.baseUrl,{params}); 
      }
  
}
