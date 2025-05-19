import { Routes } from '@angular/router';
import { AdminLayoutComponent } from './admin-layout.component';

export const admin:Routes = [
    {path:"",component:AdminLayoutComponent,children:[
        {path:"users/administration",loadChildren:()=>import('../../features/administrations/administration.routes').then(r=>r.administrations)},
        {path:"users/driver",loadChildren:()=>import('../../features/Drivers/driver.routes').then(r=>r.driver)},
        {path:"users/worker",loadChildren:()=>import('../../features/Workers/worker.routes').then(r=>r.worker)},
        {path:"bus",loadChildren:()=>import('../../features/buses/bus.routes').then(r=>r.bus)},
        {path:"stop",loadChildren:()=>import('../../features/Stops/stops.routes').then(r=>r.stop)},
    ]}
]