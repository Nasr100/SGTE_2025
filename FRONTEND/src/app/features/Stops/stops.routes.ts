import { Routes } from "@angular/router";
import { StopAdminComponent } from "./admin/stop-admin/stop-admin.component";
import { StopDetailComponent } from "./admin/stop-detail/stop-detail.component";
import { AddStopComponent } from "./admin/add-stop/add-stop.component";
import { StopFormComponent } from "./admin/stop-form/stop-form.component";

export const stop:Routes = [
    {path:"",component:StopAdminComponent},
    {path:"details/:id",component:StopDetailComponent},
    {path:"add",component:AddStopComponent}
]