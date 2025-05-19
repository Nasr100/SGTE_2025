import { Component, effect, inject, input, OnInit, signal } from '@angular/core';
import { MapComponent } from "../../../../shared/components/map/map.component";
import { MapService } from '../../../../shared/services/map/map.service';
import { OpenRouteServiceResponse } from '../../../../shared/types/Dtos/openRouteService.dto';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { FORM_IMPORTS } from '../../../../shared/imports/Form.imports';
import { StopRequest, StopResponse } from '../../../../shared/types/Dtos/stop.dto';
import { StopService } from '../../services/stop.service';
import { TuiAlertService } from '@taiga-ui/core';
import { TUI_IMPOTS } from '../../../../shared/imports/Tui.imports';

@Component({
  selector: 'app-stop-form',
  imports: [MapComponent,FORM_IMPORTS,TUI_IMPOTS],
  templateUrl: './stop-form.component.html',
  styleUrl: './stop-form.component.css'
})
export class StopFormComponent {
// point = signal<number[]|null>(null);
editFlagFromParent = input<boolean>();
editFlag:boolean = false;
stopRequest!:StopRequest;
stopResponse = input<StopResponse|null>(null);
  point = signal<[number, number] | null>(null);
  StopForm:FormGroup;
 private readonly alerts = inject(TuiAlertService);

  constructor(private mapService:MapService,private stopService:StopService){
    this.StopForm = new FormGroup({
      address : new FormControl('',[Validators.required]),
      name: new FormControl('',[Validators.required]),
      description : new FormControl('')
    })
    
    effect(() => {
    const response = this.stopResponse(); 
    this.editFlag = !!response;
    
    if (response) {
      this.editFlag = true;
      this.setFormValues(); 
      this.point.set([response.x,response.y]); 
    }else{
      this.StopForm.reset();
    }
  });
  }




getAddress(stop:any) {
  this.stopRequest = stop;
  this.StopForm.controls['address'].setValue(this.stopRequest.address);
}


  getPoint(event:any){
    const target = event.target as HTMLInputElement;
    let geocode:OpenRouteServiceResponse;
    const address = target.value;
    this.mapService.getLatlong(address).subscribe({
      next:data=>{
        const cord = data.features[0].geometry.coordinates;
          this.point.set(cord); 
      },
      error:(err)=>console.log(err),
      complete : ()=>{
      }
    });

  }

  submit(){
    if(this.editFlag){
      
    }else{
      this.getFormValues();
      this.stopService.addStop(this.stopRequest).subscribe({
        next: (data)=>{

        },
        complete:()=>{
          this.alerts.open('Stop Added succefully');
        },
        error:(err)=>{
          this.alerts.open('Stop Failed');
          console.error(err);
        }
      })
    }
  }
  

  getFormValues(){
    this.stopRequest.name = this.StopForm.controls["name"].value;
    this.stopRequest.description = this.StopForm.controls["description"].value;    
    this.stopRequest.status = "active";    
  }

  setFormValues(){
    
       const stop = this.stopResponse();
  if (!stop) return;

  this.StopForm.patchValue({
    address: stop.address || '',
    name: stop.name || '',
    description: stop.description || ''
  });
  }
}
