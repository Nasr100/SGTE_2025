import { Component } from '@angular/core';
import { TuiDialogContext } from '@taiga-ui/core';
import { injectContext } from '@taiga-ui/polymorpheus';
import { RouteRequest, RouteResponse } from '../../../../shared/types/Dtos/route.dto';
import { FORM_IMPORTS } from '../../../../shared/imports/Form.imports';
import { CommonModule } from '@angular/common';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-route-form',
  imports: [CommonModule,FORM_IMPORTS],
  templateUrl: './route-form.component.html',
  styleUrl: './route-form.component.css'
})
export class RouteFormComponent {
      public readonly context = injectContext<TuiDialogContext<RouteRequest, RouteResponse>>();
      editFlag:boolean = false;
      routeResponse:RouteResponse|null = null;
      routeRequest!:RouteRequest;
      routeForm:FormGroup;
      constructor(){
        
        this.routeForm = new FormGroup({
          name: new FormControl('',[Validators.required]),
          status: new FormControl('',[Validators.required]),
          description: new FormControl('',[]),
        })
        this.routeResponse = this.context.data;
        if(this.routeResponse){
          this.editFlag = true;
          this.setFormValues();
        }
      }


      getFormValues(){
    
    this.routeRequest.name = this.routeForm.controls["name"].value;
    this.routeRequest.description = this.routeForm.controls["description"].value;
    this.routeRequest.isActive = this.routeForm.controls["status"].value;  
  }

  setFormValues(){
    
       const route = this.routeResponse;
  if (!route) return;

  this.routeForm.patchValue({
    name: route.name || '',
    description: route.description || '',
    status: route.isActive || '',
  });
  }
}
