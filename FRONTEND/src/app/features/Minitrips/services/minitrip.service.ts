import { Injectable } from '@angular/core';
import { MinitripRequest, MinitripResponse } from '../../../shared/types/Dtos/minitrip.dto';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class MinitripService {
  baseurl:string = "";
  constructor(private client:HttpClient) { }

  addMinitrip(minitrip:MinitripRequest):Observable<MinitripResponse>{
    return this.client.post<MinitripResponse>(`${this.baseurl}`,{minitrip});
  }
}
