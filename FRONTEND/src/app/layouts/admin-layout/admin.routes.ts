import { Component } from '@angular/core';
import { Routes } from '@angular/router';
import { AdminLayoutComponent } from './admin-layout.component';
// import { AdministrationsComponent } from './Admin/administrations.component';

export const admin:Routes = [
    {path:" ",component:AdminLayoutComponent,children:[
        {path:"administration",loadChildren:()=>import('../../features/administrations/administration.routes').then(r=>r.administrations)}
    ]}
]