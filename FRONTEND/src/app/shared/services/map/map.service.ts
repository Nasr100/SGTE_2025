import { Injectable } from '@angular/core';
import { env } from '../../../../environments/environment.dev';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { OpenRouteServiceResponse } from '../../types/Dtos/openRouteService.dto';

@Injectable({
  providedIn: 'root'
})
export class MapService {

  constructor(private client:HttpClient) { }
    baseUrl:string = env.openRouteServiceBaseUrl;

    getLatlong(address:string):Observable<OpenRouteServiceResponse>{
      return this.client.get<OpenRouteServiceResponse>(`${this.baseUrl}/search?${env.openRouteServiceApiKey}&text=${address}&boundary.country=MA`);
    }

    reverseGeocode(lng: number, lat: number) {
  const url = `https://api.openrouteservice.org/geocode/reverse?${env.openRouteServiceApiKey}&point.lon=${lng}&point.lat=${lat}`;
  return this.client.get<any>(url);
}
}
