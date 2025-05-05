import { TuiRoot } from "@taiga-ui/core";
import {  Component, OnInit, } from '@angular/core';
import { RouterOutlet ,Router,Event,NavigationEnd } from '@angular/router';
import { LoginComponent } from "./pages/login/login.component";
import { AdminLayoutComponent } from "./layouts/admin-layout/admin-layout.component";
import { MAT_DIALOG_DEFAULT_OPTIONS } from "@angular/material/dialog";

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, TuiRoot, LoginComponent, AdminLayoutComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
  providers:[{provide:MAT_DIALOG_DEFAULT_OPTIONS, useValue: {hasBackdrop: false}}]
})
export class AppComponent  implements OnInit {
  title = 'FRONTEND';
  constructor(private router: Router) {
    
  }

  

  ngOnInit() {

    this.router.events.subscribe((event: Event) => {
      if (event instanceof NavigationEnd) {
        setTimeout(() => {
          if (window.HSStaticMethods) {
              window.HSStaticMethods.autoInit();
          }
        }, 300);
      }
    });

  }

    

}
