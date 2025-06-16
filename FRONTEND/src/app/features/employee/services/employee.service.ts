import { Injectable } from '@angular/core';
import { GridifyRequest, GridifyResponse } from '../../../shared/types/Dtos/gridify.dto';
import { makeRequestParams } from '../../../shared/helpers/GridifyRequestBuilder';
import { GridifyBuilder } from '../../../shared/helpers/GridifyBuilder';
import { EmployeeRequest, EmployeeResponse } from '../../../shared/types/Dtos/Employee.dto';
import { Observable } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
  baseUrl:string = ""
  constructor(private client:HttpClient) { }
  geEmployees(query:GridifyBuilder):Observable<GridifyResponse<EmployeeResponse>>{
      const params = makeRequestParams(query);
      return this.client.get<GridifyResponse<EmployeeResponse>>(this.baseUrl,{params}); 
    }

    addEmployee(employee:EmployeeRequest):Observable<EmployeeResponse>{
      return this.client.post<EmployeeResponse>(`${this.baseUrl}`,{employee});
    }

    getEmployeeById(id:number):Observable<EmployeeResponse>{
      return this.client.get<EmployeeResponse>(`${this.baseUrl}/${id}`);
    }

    updateEmployee(id:number,employee:EmployeeRequest):Observable<EmployeeResponse>{
      return this.client.put<EmployeeResponse>(`${this.baseUrl}/${id}`,{employee});
    }

    deleteEmployee(id:number){
      return this.client.delete(`${this.baseUrl}/${id}`);
    }

    getEmployeeByRole(role:string):Observable<EmployeeResponse[]>{
      const params =  new HttpParams().set("role",role);
      return this.client.get<EmployeeResponse[]>(`${this.baseUrl}`,{params});
    }

}
