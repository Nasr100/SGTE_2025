import { Component, OnInit } from '@angular/core';
import { FORM_IMPORTS } from '../../../../shared/imports/Form.imports';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { GridifyBuilder } from '../../../../shared/helpers/GridifyBuilder';
import { GroupResponse } from '../../../../shared/types/Dtos/group.dto';
import { GroupService } from '../../../groups/services/group.service';
import { injectContext } from '@taiga-ui/polymorpheus';
import { TuiDialogContext } from '@taiga-ui/core';
import { EmployeeRequest, EmployeeResponse } from '../../../../shared/types/Dtos/Employee.dto';
import { StopService } from '../../../Stops/services/stop.service';
import { StopResponse } from '../../../../shared/types/Dtos/stop.dto';

@Component({
  selector: 'app-employee-form',
  imports: [FORM_IMPORTS],
  templateUrl: './employee-form.component.html',
  styleUrl: './employee-form.component.css'
})
export class EmployeeFormComponent implements OnInit{
    employeeForm!:FormGroup;
    groups!:GroupResponse[];
    stops!:StopResponse[];
    employee!:EmployeeRequest ;
    employeeRes!:EmployeeResponse;
    gridifyBuilder:GridifyBuilder ;
    groupsByRole!:GroupResponse[];

  // submit = output<any>();  
  // close = output<void>(); 
    public readonly context = injectContext<TuiDialogContext<EmployeeRequest, EmployeeResponse>>();
  
  constructor(private groupService:GroupService,private stopService:StopService){
    this.gridifyBuilder=new GridifyBuilder();
    this.gridifyBuilder.setPagination(null);
    this.employeeForm = new FormGroup({
      email:new FormControl('',[Validators.email,Validators.required]),
      password:new FormControl('',[Validators.required,Validators.minLength(8)]),
      phoneNumber:new FormControl('',[Validators.required]),
      firstName:new FormControl('',[Validators.required]),
      lastName:new FormControl('',[Validators.required]),
      badgeNumber:new FormControl('',[Validators.required]),
      address:new FormControl('',[Validators.required]),
      status:new FormControl('',[Validators.required]),
      group:new FormControl('',[Validators.required]),
      stop:new FormControl('',[Validators.required]),
      role:new FormControl('',[Validators.required]),
    })
    if(this.context.data){
      this.employeeRes = this.context.data;
      this.employeeForm.controls['email'].setValue(this.employee.email);
      this.employeeForm.controls['password'].setValue(this.employee.password);
      this.employeeForm.controls['phoneNumber'].setValue(this.employee.phoneNumber);
      this.employeeForm.controls['firstName'].setValue(this.employee.firstName);
      this.employeeForm.controls['lastName'].setValue(this.employee.lastName);
      this.employeeForm.controls['badgeNumber'].setValue(this.employee.badgeNumber);
      this.employeeForm.controls['address'].setValue(this.employee.address);
      this.employeeForm.controls['status'].setValue(this.employee.address);
      this.employeeForm.controls['group'].setValue(this.employee.groupId);
      this.employeeForm.controls['stop'].setValue(this.employee.stopId);
      this.employeeForm.controls['role'].setValue(this.employee.role);

    }
  }
   ngOnInit(): void {
    this.groupService.getGroupByRole("worker").subscribe({
      next:(data)=> this.groups = data.data
    })
    this.getStops();
  }

   onSubmit(){
      if(this.employeeForm.valid){
        let  employeeReq:EmployeeRequest = {
          email: this.employeeForm.controls['email'].value,
          password: this.employeeForm.controls['password'].value,
          phoneNumber: this.employeeForm.controls['phoneNumber'].value,
          firstName: this.employeeForm.controls['firstName'].value,
          lastName: this.employeeForm.controls['lastName'].value,
          badgeNumber: this.employeeForm.controls['badgeNumber'].value,
          address: this.employeeForm.controls['address'].value,
          status: this.employeeForm.controls['status'].value,
          role: this.employeeForm.controls['role'].value,
          stopId: this.employeeForm.controls['stop'].value,
          groupId: this.employeeForm.controls['group'].value
        };
        this.context.completeWith(employeeReq);
      }
    }
    getStops(){
      this.stopService.getStops(this.gridifyBuilder).subscribe({
        next : (data)=>this.stops = data.data
      })
    }

    chooseGroups(event:any):void{
      const role = event.target.value;
      this.groupsByRole =  this.groups.filter(g=>g.role==role);
    }
}
