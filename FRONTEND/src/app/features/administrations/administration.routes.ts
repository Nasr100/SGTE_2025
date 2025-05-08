import { Routes } from '@angular/router';
import { AdministrationsComponent } from './Admin/administrations.component';
import { AdministartionDetailComponent } from './Admin/administartion-detail/administartion-detail.component';

export const administrations:Routes = [
    {path:"",component:AdministrationsComponent},
    {path:"details/:id",component:AdministartionDetailComponent},
]