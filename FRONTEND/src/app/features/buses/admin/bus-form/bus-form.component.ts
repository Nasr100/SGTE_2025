import { Component, output } from '@angular/core';
import { FORM_IMPORTS } from '../../../../shared/imports/Form.imports';
import { TuiDialogContext } from '@taiga-ui/core';
import { injectContext } from '@taiga-ui/polymorpheus';
import { BusRequest, BusResponse } from '../../../../shared/types/Dtos/bus.dto';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-bus-form',
  imports: [FORM_IMPORTS],
  templateUrl: './bus-form.component.html',
  styleUrl: './bus-form.component.css'
})
export class BusFormComponent {
busForm!:FormGroup;
  // submit = output<any>();  
  // close = output<void>(); 
  public readonly context = injectContext<TuiDialogContext<BusRequest, BusResponse>>();
  bus?:BusRequest ;

  constructor(){
    this.busForm = new FormGroup({
      number:new FormControl('',[Validators.email,Validators.required]),
      plate:new FormControl('',[Validators.required,Validators.minLength(8)]),
      startYear:new FormControl(''),
      status:new FormControl('',[Validators.required]),
    })

    if(this.context.data){
      this.bus = this.context.data;
      this.busForm.controls['number'].setValue(this.bus?.number);
      this.busForm.controls['plate'].setValue(this.bus?.plate);
      this.busForm.controls['startYear'].setValue(this.bus?.startYear);
      this.busForm.controls['status'].setValue(this.bus?.status);
    }
  }

  onSubmit(){
      if(this.busForm.valid){
        let  busReq:BusRequest = {
           number :this.busForm.controls['number'].value,
          plate : this.busForm.controls['plate'].value,
            startYear : this.busForm.controls['startYear'].value,
            status : this.busForm.controls['status'].value,
        };
        this.context.completeWith(busReq);
      }
    }

}


