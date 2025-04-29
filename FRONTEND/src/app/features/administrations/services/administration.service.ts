import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { env } from '../../../../environments/environment.dev';
import { GridifyRequest, GridifyResponse } from '../../../shared/types/Dtos/gridify.dto';
import { Administrations } from '../types/administration.model';
import { Observable } from 'rxjs';
import { AdministartionResponse } from '../../../shared/types/Dtos/administration.dto';

@Injectable({
  providedIn: 'root'
})
export class AdministrationService {

  constructor(private client:HttpClient) { }
   
  url:string = `${env.apiBaseUrl}/Administration`;

  getAdministrations(query:GridifyRequest):Observable<GridifyResponse<AdministartionResponse>>{
    const searchfilter = query.search ? `employee.firstname=*${query.search}|employee.lastname=*${query.search}` : '';
let concatFilter:string = "";
    let params = new HttpParams()
      .set('page', query.pagination.getPageNumber())
      .set('pageSize', query.pagination.getPageSize());

    if (query.filters) {
      concatFilter = query.filters;
      params = params.set('filter', concatFilter);

    }
    if(query.search){
      if(concatFilter.length != 0){
        concatFilter += 'and';
      }
      concatFilter +=searchfilter;
      params = params.set('filter', concatFilter);
    }

    if (query.sort) {
      params = params.set('sort', query.sort);
    }
    return this.client.get<GridifyResponse<AdministartionResponse>>(this.url, {params});
  }
}
