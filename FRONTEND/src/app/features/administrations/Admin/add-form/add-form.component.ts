import { Component, Inject, input, output } from '@angular/core';
import { FORM_IMPORTS } from '../../../../shared/imports/Form.imports';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { AdministartionRequest, AdministartionResponse } from '../../../../shared/types/Dtos/administration.dto';
import { TuiDialogContext } from '@taiga-ui/core';
import { POLYMORPHEUS_CONTEXT } from '@tinkoff/ng-polymorpheus';
import {injectContext} from '@taiga-ui/polymorpheus';


@Component({
  selector: 'app-add-form',
  imports: [FORM_IMPORTS],
  templateUrl: './add-form.component.html',
  styleUrl: './add-form.component.css',
  standalone:true
})
export class AddFormComponent {
  administrationForm!:FormGroup;
  submit = output<any>();  
  close = output<void>(); 
  administration!:AdministartionRequest ;
  public readonly context = injectContext<TuiDialogContext<AdministartionRequest, AdministartionResponse>>();

  constructor() {
    this.administrationForm = new FormGroup({
      email:new FormControl('',[Validators.email,Validators.required]),
      password:new FormControl('',[Validators.required,Validators.minLength(8)]),
      phoneNumber:new FormControl('',[Validators.required]),
      firstName:new FormControl('',[Validators.required]),
      lastName:new FormControl('',[Validators.required]),
      badgeNumber:new FormControl('',[Validators.required]),
      address:new FormControl('',[Validators.required]),
      dep:new FormControl('',[Validators.required]),
      isAdmin:new FormControl('',[]),
    })

    if (this.context.data) {
      this.administration = this.context.data;
      this.administrationForm.controls['email'].setValue(this.administration.employee.email);
      this.administrationForm.controls['password'].setValue(this.administration.employee.password);
      this.administrationForm.controls['phoneNumber'].setValue(this.administration.employee.phoneNumber);
      this.administrationForm.controls['firstName'].setValue(this.administration.employee.firstName);
      this.administrationForm.controls['lastName'].setValue(this.administration.employee.lastName);
      this.administrationForm.controls['badgeNumber'].setValue(this.administration.employee.badgeNumber);
      this.administrationForm.controls['address'].setValue(this.administration.employee.address);
      this.administrationForm.controls['dep'].setValue(this.administration.departement);
      this.administrationForm.controls['isAdmin'].setValue(this.administration.employee.isAdmin);


    }
  }

  onSubmit(){

    if(this.administrationForm.valid){

      let  AdministrationReq:AdministartionRequest = {
        employee:{
         email :this.administrationForm.controls['email'].value,
     password : this.administrationForm.controls['password'].value,
      phoneNumber : this.administrationForm.controls['phoneNumber'].value,
      firstName : this.administrationForm.controls['firstName'].value,
      lastName : this.administrationForm.controls['lastName'].value,
      badgeNumber : this.administrationForm.controls['badgeNumber'].value,
      address : this.administrationForm.controls['address'].value,
      isAdmin : this.administrationForm.controls['isAdmin'].value
        },
        departement : this.administrationForm.controls['dep'].value,

      };
      
      this.context.completeWith(AdministrationReq);
    }
  }


}


