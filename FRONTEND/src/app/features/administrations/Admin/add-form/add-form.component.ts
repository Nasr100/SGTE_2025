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
  // administration:AdministartionResponse ;
  public readonly context = injectContext<TuiDialogContext<AdministartionResponse, any>>();

  constructor() {
    this.administrationForm = new FormGroup({
      email:new FormControl('',[Validators.email,Validators.required]),
      password:new FormControl('',[Validators.required,Validators.minLength(8)]),
      phoneNumber:new FormControl('',[Validators.required]),
      firstName:new FormControl('',[Validators.required]),
      lastName:new FormControl('',[Validators.required]),
      badgeNumber:new FormControl('',[Validators.required]),
      address:new FormControl('',[Validators.required]),
      isAdmin:new FormControl('',[]),
    })

    if (this.context.data) {
      // this.administration = this.context.data.AdministrationMember;
      console.log(this.context.data)
      // this.administrationForm.patchValue(this.context.data.id);
    }
  }

  onSubmit(){
    if(this.administrationForm.valid){
      this.context.completeWith(this.administrationForm.value);
    }
  }
}


