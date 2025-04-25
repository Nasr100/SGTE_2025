import { TuiRoot } from "@taiga-ui/core";
import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { LoginComponent } from "./pages/login/login.component";
import { AdminLayoutComponent } from "./layouts/admin-layout/admin-layout.component";

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, TuiRoot, LoginComponent, AdminLayoutComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'FRONTEND';
}
