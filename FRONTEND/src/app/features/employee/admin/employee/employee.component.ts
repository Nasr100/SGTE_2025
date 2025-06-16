import { Component, effect, inject, signal, TemplateRef, ViewChild } from '@angular/core';
import { RouterModule } from '@angular/router';
import { FORM_IMPORTS } from '../../../../shared/imports/Form.imports';
import { TUI_IMPOTS } from '../../../../shared/imports/Tui.imports';
import { EmployeeRequest, EmployeeResponse } from '../../../../shared/types/Dtos/Employee.dto';
import { Pagination } from '../../../../shared/helpers/pagination';
import { EmployeeService } from '../../services/employee.service';
import { TuiAlertService, TuiDialogService } from '@taiga-ui/core';
import { GridifyBuilder } from '../../../../shared/helpers/GridifyBuilder';
import { EmployeeFormComponent } from "../employee-form/employee-form.component";

@Component({
  selector: 'app-employee',
  imports: [TUI_IMPOTS, FORM_IMPORTS, RouterModule, EmployeeFormComponent],
  templateUrl: './employee.component.html',
  styleUrl: './employee.component.css'
})
export class EmployeeComponent {
    @ViewChild('employeeForm') employeeForm!: TemplateRef<any>;
employees = signal<EmployeeResponse[]>([]);
  search=signal('');
  pagination= signal( new Pagination());
  count = signal(0);
  gridifyBuilder = signal(new GridifyBuilder())

  constructor(private readonly employeeService:EmployeeService,private readonly dialogService: TuiDialogService){
      effect(()=>{
     this.gridifyBuilder().setPagination(this.pagination());
    this.employeeService.geEmployees(this.gridifyBuilder()).subscribe(data=>{
      this.employees.set(data.data)
          this.count.set(data.count)
    })

  })  
  }
Onsearch(event:any){
    const target = event.target as HTMLInputElement;
    this.search.set(target.value)
    this.gridifyBuilder().addCondition("name","=*",this.search());
    this.pagination.update(p =>{
      const newPagination = new Pagination();
        newPagination.setPagination(p);
        newPagination.resetPageNumber();
      return newPagination;
    }) 
  }

   nextPage(){
    this.pagination.update(p=>{
      const newPagination = new Pagination();
      newPagination.setPagination(p);
      if(p.getCurrent()<this.count()){
        newPagination.nexPage();
      }
      return newPagination;
    })
   }
  
   prevPage(){
    this.pagination.update(p =>{
      const newPagination = new Pagination();
        newPagination.setPagination(p);
        newPagination.prevPage();
      return newPagination;
    })
   }
statusFilter(event:any){
      const target = event.target as HTMLInputElement;
      if(target.value)
      this.gridifyBuilder().addCondition("status","=",target.value);
      this.pagination.update(p =>{
      const newPagination = new Pagination();
        newPagination.setPagination(p);
        newPagination.resetPageNumber();
      return newPagination;
    }) 
   }
  //  private readonly dialogs = inject(TuiDialogService);
      private readonly alerts = inject(TuiAlertService);
openAddForm(){
         this.dialogService.open<EmployeeRequest>(this.employeeForm,{
               size: 'page',
               dismissible: true,
               closeable: true,
               data:null
             }).subscribe((result: EmployeeRequest) => {
               if(result){
                   
                   this.employeeService.addEmployee(result).subscribe({
                     next: (res) => {
                       this.employees.update((current) => [...current, res]);
                       this.alerts.open("Successfully added");
                     },
                     error: (err) => {
                       this.alerts.open('Add failed');
                       console.error('Add failed:', err);
                     }
                   });
             }
           });
        }
}


