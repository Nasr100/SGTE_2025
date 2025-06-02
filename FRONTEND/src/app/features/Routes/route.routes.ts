import { Routes } from "@angular/router";
import { RouteAdminComponent } from "./admin/route-admin/route-admin.component";
import { DetailsRouteComponent } from "./admin/details-route/details-route.component";

export const route:Routes = [
    {path:"",component:RouteAdminComponent},
    {path:"details/:id",component:DetailsRouteComponent},
    // {path:"add",component:AddStopComponent}
]