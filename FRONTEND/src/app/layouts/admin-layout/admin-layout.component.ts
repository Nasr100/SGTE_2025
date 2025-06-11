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
import { AddFormComponent } from "../../features/administrations/Admin/add-form/add-form.component";
import { StopDetailComponent } from "../../features/Stops/admin/stop-detail/stop-detail.component";
import { AddStopComponent } from "../../features/Stops/admin/add-stop/add-stop.component";
import { RouteAdminComponent } from "../../features/Routes/admin/route-admin/route-admin.component";
import { RouteMapComponent } from "../../shared/components/route-map/route-map.component";
import { DetailsRouteComponent } from "../../features/Routes/admin/details-route/details-route.component";
import { HomeTripsComponent } from "../../features/Trips/admin/home-trips/home-trips.component";
import { TripDetailsComponent } from "../../features/Trips/admin/trip-details/trip-details.component";
import { MinitripCardComponent } from "../../features/Minitrips/admin/minitrip-card/minitrip-card.component";

@Component({
  selector: 'app-admin-layout',
  imports: [HeaderComponent, SideBarComponent, RouterOutlet, AdministrationsComponent, FORM_IMPORTS, DriverAdminComponent, AdministartionDetailComponent, MapComponent, MapCardComponent, StopAdminComponent, StopFormComponent, AddFormComponent, StopDetailComponent, AddStopComponent, RouteAdminComponent, RouteMapComponent, DetailsRouteComponent, HomeTripsComponent, TripDetailsComponent, MinitripCardComponent],
  templateUrl: './admin-layout.component.html',
  styleUrl: './admin-layout.component.css'
})
export class AdminLayoutComponent {

}
