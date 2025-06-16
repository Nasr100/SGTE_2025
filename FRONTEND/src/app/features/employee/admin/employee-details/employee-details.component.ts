import { Component, inject, TemplateRef, ViewChild } from '@angular/core';
import { EmployeeFormComponent } from "../employee-form/employee-form.component";
import { EmployeeService } from '../../services/employee.service';
import { ActivatedRoute, Router } from '@angular/router';
import { TuiAlertService, TuiDialogService } from '@taiga-ui/core';
import { EmployeeRequest, EmployeeResponse } from '../../../../shared/types/Dtos/Employee.dto';
import { catchError, map, switchMap, tap, throwError } from 'rxjs';
import { TUI_CONFIRM, TuiConfirmData } from '@taiga-ui/kit';
import { TuiResponsiveDialogService } from '@taiga-ui/addon-mobile';

@Component({
  selector: 'app-employee-details',
  imports: [EmployeeFormComponent],
  templateUrl: './employee-details.component.html',
  styleUrl: './employee-details.component.css'
})
export class EmployeeDetailsComponent {
  @ViewChild('editForm') EditForm!: TemplateRef<any>;
  employeeId!:number;
  employee!:EmployeeResponse;
  constructor(private employeeService:EmployeeService,private route: ActivatedRoute,private readonly dialogService: TuiDialogService,private router:Router ){

  }
  ngOnInit(): void {
    this.route.paramMap.subscribe(param=>{
      this.employeeId= Number(param.get('id'));
    })

    this.employeeService.getEmployeeById(this.employeeId).subscribe(res=>{
      this.employee = res;
      console.log(res)

    })
  }
  private readonly alerts = inject(TuiAlertService);
  private readonly dialogs = inject(TuiResponsiveDialogService);

  openEditForm() {
        this.dialogService.open<EmployeeRequest>(this.EditForm,{
          size: 'page',
          dismissible: true,
          closeable: true,
          data:this.employee
        }).subscribe((result: EmployeeRequest) => {
          if(result){
            this.employeeService.updateEmployee(this.employeeId,result).pipe(
              tap((w)=>{
                this.employee = w;
                this.alerts.open("Successfully Updated");
              }),
              catchError(err=>{
                this.alerts.open('Update failed');
              return throwError(err);
            })
            ).subscribe()
          }
      });
      }

      onClick() {
                    const data: TuiConfirmData = {
                      content:
                      '',
                      yes: 'Delete',
                      no: 'Cancel',
                      appearance: "error"
                  };
              
                  let confirm:any =this.dialogs
                  .open<boolean>(TUI_CONFIRM, {
                      label: 'Are you sure you want to delete this employee?',
                      size: 'l',
                      data,
                      dismissible: false,
                      closeable: false,
                      
                  }).pipe(map(res=>{
                    if(res){
                      return this.employeeService.deleteEmployee(this.employeeId).pipe(
                        switchMap((res) => this.alerts.open("Successfully deleted")),
                        catchError(err=>{
                          this.alerts.open('Delete failed');
                        return throwError(err);})
                      ).subscribe();
                    }
                    return null;
                  }))
                  .subscribe(res=>{
                    // this.router.navigate(["/Admin/driver"]);
                  });
                  
                  
                }
}
