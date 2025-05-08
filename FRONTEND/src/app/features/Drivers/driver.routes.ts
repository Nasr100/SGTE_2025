import { Routes } from '@angular/router';
import { DriverAdminComponent } from './admin/driver-admin/driver-admin.component';
import { DriverDetailsComponent } from './admin/driver-details/driver-details.component';

export const driver:Routes = [
    {path:"",component:DriverAdminComponent},
    {path:"details/:id",component:DriverDetailsComponent},
]