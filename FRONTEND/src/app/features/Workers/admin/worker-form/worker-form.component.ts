import { Component, output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { WorkerRequest, WorkerResponse } from '../../../../shared/types/Dtos/worker.dto';
import { TuiDialogContext } from '@taiga-ui/core';
import { injectContext } from '@taiga-ui/polymorpheus';
import { FORM_IMPORTS } from '../../../../shared/imports/Form.imports';

@Component({
  selector: 'app-worker-form',
  imports: [FORM_IMPORTS],
  templateUrl: './worker-form.component.html',
  styleUrl: './worker-form.component.css'
})
export class WorkerFormComponent {
  workerForm!:FormGroup;
  // submit = output<any>();  
  // close = output<void>(); 
  public readonly context = injectContext<TuiDialogContext<WorkerRequest, WorkerResponse>>();
  worker?:WorkerRequest ;
  constructor(){
    this.workerForm = new FormGroup({
      email:new FormControl('',[Validators.email,Validators.required]),
      password:new FormControl('',[Validators.required,Validators.minLength(8)]),
      phoneNumber:new FormControl('',[Validators.required]),
      firstName:new FormControl('',[Validators.required]),
      lastName:new FormControl('',[Validators.required]),
      badgeNumber:new FormControl('',[Validators.required]),
      address:new FormControl('',[Validators.required]),
    })
    if(this.context.data){
      this.worker = this.context.data;
      this.workerForm.controls['email'].setValue(this.worker?.employee.email);
      this.workerForm.controls['password'].setValue(this.worker?.employee.password);
      this.workerForm.controls['phoneNumber'].setValue(this.worker?.employee.phoneNumber);
      this.workerForm.controls['firstName'].setValue(this.worker?.employee.firstName);
      this.workerForm.controls['lastName'].setValue(this.worker?.employee.lastName);
      this.workerForm.controls['badgeNumber'].setValue(this.worker?.employee.badgeNumber);
      this.workerForm.controls['address'].setValue(this.worker?.employee.address);
    }
  }

  onSubmit(){
      if(this.workerForm.valid){
        let  driverReq:WorkerRequest = {
          employee:{
           email :this.workerForm.controls['email'].value,
       password : this.workerForm.controls['password'].value,
        phoneNumber : this.workerForm.controls['phoneNumber'].value,
        firstName : this.workerForm.controls['firstName'].value,
        lastName : this.workerForm.controls['lastName'].value,
        badgeNumber : this.workerForm.controls['badgeNumber'].value,
        address : this.workerForm.controls['address'].value,
        isAdmin : false
          },
        };
        this.context.completeWith(driverReq);
      }
    }

}



