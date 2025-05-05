import { Component, output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { FORM_IMPORTS } from '../../../../shared/imports/Form.imports';

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
  }

  onSubmit(){
    if(this.driverForm.valid){
      console.log(this.driverForm.value)
    }
  }
}
