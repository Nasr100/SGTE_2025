import { Component } from '@angular/core';
import { FormGroup,FormBuilder, Validators, FormControl } from '@angular/forms';
import { TUI_IMPOTS } from '../../shared/imports/Tui.imports';
import { FORM_IMPORTS } from '../../shared/imports/Form.imports';
import { LoginRequestDto } from '../../core/auth/Login-request.dto';
import { AuthService } from '../../core/auth/auth.service';
import { tap } from 'rxjs';
import { SessionService } from '../../core/auth/session.service';
import { env } from '../../../environments/environment.dev';

@Component({
  selector: 'app-login',
  imports: [TUI_IMPOTS,FORM_IMPORTS],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
  standalone : true

})
export class LoginComponent {
  loginForm!:FormGroup;
  constructor(private authService:AuthService,private sessionService:SessionService){
    this.loginForm = new FormGroup({
        Email:new FormControl('',[Validators.email,Validators.required]),
        Password:new FormControl('',[Validators.required]),
        LayoutType:new FormControl('',[Validators.required]),
    })
  }

  login(){
   let creds:LoginRequestDto = {email:this.loginForm.get("Email")?.value,password:this.loginForm.get("Password")?.value};
    this.authService.getToken(creds).pipe(
      tap({
        next:(key)=>this.sessionService.setItem(`${env.authTokenKey}`,key),
        error:(error)=>console.log(error)
      })
  ).subscribe()
  
  
  }
}
