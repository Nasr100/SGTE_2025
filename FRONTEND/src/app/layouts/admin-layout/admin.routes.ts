import { Routes } from '@angular/router';
import { AdminLayoutComponent } from './admin-layout.component';

export const admin:Routes = [
    {path:"",component:AdminLayoutComponent,children:[

        {path:"bus",loadChildren:()=>import('../../features/buses/bus.routes').then(r=>r.bus)},
        {path:"stop",loadChildren:()=>import('../../features/Stops/stops.routes').then(r=>r.stop)},
        {path:"employee",loadChildren:()=>import('../../features/employee/employee.routes').then(r=>r.employee)},
    ]}
]