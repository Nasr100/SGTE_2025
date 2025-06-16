import { Routes } from "@angular/router";
import { EmployeeComponent } from "./admin/employee/employee.component";
import { EmployeeDetailsComponent } from "./admin/employee-details/employee-details.component";

export const employee:Routes = [
    {path:"",component:EmployeeComponent},
    {path:"details/:id",component:EmployeeDetailsComponent},
]