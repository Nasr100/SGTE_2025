import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { env } from '../../../../environments/environment.dev';
import { GridifyRequest, GridifyResponse } from '../../../shared/types/Dtos/gridify.dto';
import { WorkerRequest, WorkerResponse } from '../../../shared/types/Dtos/worker.dto';
import { Observable } from 'rxjs';
import { makeEmployeeRequest } from '../../../shared/helpers/GridifyRequestBuilder';

@Injectable({
  providedIn: 'root'
})
export class WorkerService {

  constructor(private client:HttpClient) { }
    url:string = `${env.apiBaseUrl}/users/worker`;

  getWorkers(query:GridifyRequest):Observable<GridifyResponse<WorkerResponse>>{
    const params = makeEmployeeRequest(query,[{field:"employee.badgeNumber",type:"string"},{field:"employee.firstname",type:"string"},{field:"employee.lastname",type:"string"}]);
    return this.client.get<GridifyResponse<WorkerResponse>>(this.url,{params}); 
  }

  addWorker(worker:WorkerRequest):Observable<WorkerResponse>{
    return this.client.post<WorkerResponse>(this.url,worker);
  }

  updateWorker(id:number,driver:WorkerRequest):Observable<WorkerResponse>{
    return this.client.put<WorkerResponse>(`${this.url}/${id}`,driver);
  }

  deleteWorker(id:Number){
    return this.client.delete(`${this.url}/${id}`);
  }

  getWorkerById(id:Number):Observable<WorkerResponse>{
    return this.client.get<WorkerResponse>(`${this.url}/${id}`)
  }
}
