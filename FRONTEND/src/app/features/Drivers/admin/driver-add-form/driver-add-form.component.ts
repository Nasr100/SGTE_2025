import { Component, output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { FORM_IMPORTS } from '../../../../shared/imports/Form.imports';
import { TuiDialogContext } from '@taiga-ui/core';
import { injectContext } from '@taiga-ui/polymorpheus';
import { AdministartionRequest, AdministartionResponse } from '../../../../shared/types/Dtos/administration.dto';
import { DriverRequest, DriverResponse } from '../../../../shared/types/Dtos/driver.dto';

@Component({
  selector: 'app-driver-add-form',
  imports: [FORM_IMPORTS],
  templateUrl: './driver-add-form.component.html',
  styleUrl: './driver-add-form.component.css'
})
export class DriverAddFormComponent {
  driverForm!:FormGroup;
  submit = output<any>();  
  close = output<void>(); 
  public readonly context = injectContext<TuiDialogContext<DriverRequest, DriverResponse>>();
  driver?:DriverRequest ;

  constructor(){
    this.driverForm = new FormGroup({
      email:new FormControl('',[Validators.email,Validators.required]),
      password:new FormControl('',[Validators.required,Validators.minLength(8)]),
      phoneNumber:new FormControl('',[Validators.required]),
      firstName:new FormControl('',[Validators.required]),
      lastName:new FormControl('',[Validators.required]),
      badgeNumber:new FormControl('',[Validators.required]),
      address:new FormControl('',[Validators.required]),
      licenceNumber:new FormControl('',[Validators.required]),
      permisType:new FormControl('',[Validators.required]),
    })

    if(this.context.data){
      this.driver = this.context.data;
      this.driverForm.controls['email'].setValue(this.driver?.employee.email);
      this.driverForm.controls['password'].setValue(this.driver?.employee.password);
      this.driverForm.controls['phoneNumber'].setValue(this.driver?.employee.phoneNumber);
      this.driverForm.controls['firstName'].setValue(this.driver?.employee.firstName);
      this.driverForm.controls['lastName'].setValue(this.driver?.employee.lastName);
      this.driverForm.controls['badgeNumber'].setValue(this.driver?.employee.badgeNumber);
      this.driverForm.controls['address'].setValue(this.driver?.employee.address);
      this.driverForm.controls['licenceNumber'].setValue(this.driver?.licenceNumber);
      this.driverForm.controls['permisType'].setValue(this.driver?.permisType);

    }
  }

  onSubmit(){
    if(this.driverForm.valid){
      let  driverReq:DriverRequest = {
        employee:{
         email :this.driverForm.controls['email'].value,
     password : this.driverForm.controls['password'].value,
      phoneNumber : this.driverForm.controls['phoneNumber'].value,
      firstName : this.driverForm.controls['firstName'].value,
      lastName : this.driverForm.controls['lastName'].value,
      badgeNumber : this.driverForm.controls['badgeNumber'].value,
      address : this.driverForm.controls['address'].value,
      isAdmin : false
        },
        licenceNumber : this.driverForm.controls['licenceNumber'].value,
        permisType : this.driverForm.controls['permisType'].value,

      };
      this.context.completeWith(driverReq);
    }
  }
}
