import { Injectable } from '@angular/core';
import { env } from '../../../environments/environment.dev';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { LoginRequestDto } from './Login-request.dto';
@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private AuthUrl:string = `${env.apiBaseUrl}/Auth`;
  constructor(private http: HttpClient) { }

    public getToken(loginRequest:LoginRequestDto){
      return this.http.post(`${this.AuthUrl}/Login`,loginRequest);
    }


}
