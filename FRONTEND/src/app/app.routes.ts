import { Routes } from '@angular/router';
import { LoginComponent } from './pages/login/login.component';
import { AdminLayoutComponent } from './layouts/admin-layout/admin-layout.component';

export const routes: Routes = [
    {path:"login",component:LoginComponent},
    {path:"Admin",loadChildren:()=>import('./features/administrations/administration.routes').then(r=>r.administrations)}
];
