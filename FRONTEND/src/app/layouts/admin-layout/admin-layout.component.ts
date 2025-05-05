import { Component } from '@angular/core';
import { HeaderComponent } from "./header/header.component";
import { SideBarComponent } from "./side-bar/side-bar.component";
import { RouterOutlet  } from '@angular/router';
import { AdministrationsComponent } from "../../features/administrations/Admin/administrations.component";
import { FORM_IMPORTS } from '../../shared/imports/Form.imports';
import { DriverAdminComponent } from "../../features/Drivers/admin/driver-admin/driver-admin.component";
import { AdministartionDetailComponent } from "../../features/administrations/Admin/administartion-detail/administartion-detail.component";

@Component({
  selector: 'app-admin-layout',
  imports: [HeaderComponent, SideBarComponent, RouterOutlet, AdministrationsComponent, FORM_IMPORTS, DriverAdminComponent, AdministartionDetailComponent],
  templateUrl: './admin-layout.component.html',
  styleUrl: './admin-layout.component.css'
})
export class AdminLayoutComponent {

}
