import { Routes } from "@angular/router";
import { AdminWorkerComponent } from "./admin/admin-worker/admin-worker.component";
import { WorkerDetailsComponent } from "./admin/worker-details/worker-details.component";

export const worker:Routes = [
    {path:"",component:AdminWorkerComponent},
    {path:"details/:id",component:WorkerDetailsComponent}
]