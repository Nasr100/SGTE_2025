import { Component } from '@angular/core';
import { HeaderComponent } from "./header/header.component";
import { SideBarComponent } from "./side-bar/side-bar.component";
import { RouterOutlet  } from '@angular/router';
import { AdministrationsComponent } from "../../features/administrations/Admin/administrations.component";
import { FORM_IMPORTS } from '../../shared/imports/Form.imports';
import { DriverAdminComponent } from "../../features/Drivers/admin/driver-admin/driver-admin.component";
import { AdministartionDetailComponent } from "../../features/administrations/Admin/administartion-detail/administartion-detail.component";
import { MapComponent } from "../../shared/components/map/map.component";
import { MapCardComponent } from "../../features/Stops/admin/map-card/map-card.component";
import { StopAdminComponent } from "../../features/Stops/admin/stop-admin/stop-admin.component";
import { StopFormComponent } from "../../features/Stops/admin/stop-form/stop-form.component";

@Component({
  selector: 'app-admin-layout',
  imports: [HeaderComponent, SideBarComponent, RouterOutlet, AdministrationsComponent, FORM_IMPORTS, DriverAdminComponent, AdministartionDetailComponent, MapComponent, MapCardComponent, StopAdminComponent, StopFormComponent],
  templateUrl: './admin-layout.component.html',
  styleUrl: './admin-layout.component.css'
})
export class AdminLayoutComponent {

}
