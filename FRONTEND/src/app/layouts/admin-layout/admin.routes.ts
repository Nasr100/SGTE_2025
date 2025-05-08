import { Routes } from '@angular/router';
import { AdminLayoutComponent } from './admin-layout.component';

export const admin:Routes = [
    {path:"",component:AdminLayoutComponent,children:[
        {path:"administration",loadChildren:()=>import('../../features/administrations/administration.routes').then(r=>r.administrations)},
        {path:"driver",loadChildren:()=>import('../../features/Drivers/driver.routes').then(r=>r.driver)},
        {path:"worker",loadChildren:()=>import('../../features/Workers/worker.routes').then(r=>r.worker)},
    ]}
]