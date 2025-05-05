import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { env } from '../../../../environments/environment.dev';
import { GridifyRequest, GridifyResponse } from '../../../shared/types/Dtos/gridify.dto';
import { DriverRequest, DriverResponse } from '../../../shared/types/Dtos/driver.dto';
import { Observable } from 'rxjs';
import { makeEmployeeRequest } from '../../../shared/helpers/GridifyRequestBuilder';

@Injectable({
  providedIn: 'root'
})
export class DriverService {

  constructor(private client:HttpClient) { }
    url:string = `${env.apiBaseUrl}/Driver`;

    getDrivers(query:GridifyRequest):Observable<GridifyResponse<DriverResponse>>{
      const params = makeEmployeeRequest(query,[{field:"employee.badgeNumber",type:"string"},{field:"employee.firstname",type:"string"},{field:"employee.lastname",type:"string"}]);
          return this.client.get<GridifyResponse<DriverResponse>>(this.url,{params}); 
    }

    addDriver(driver:DriverRequest){
      return this.client.post(this.url,driver);
    }

}
