import { Component, inject, signal } from '@angular/core';
import { MapComponent } from "../../../../shared/components/map/map.component";
import { MapService } from '../../../../shared/services/map/map.service';
import { OpenRouteServiceResponse } from '../../../../shared/types/Dtos/openRouteService.dto';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { FORM_IMPORTS } from '../../../../shared/imports/Form.imports';
import { StopRequest } from '../../../../shared/types/Dtos/stop.dto';
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
editFlag:number = 0;
stop!:StopRequest;
  point = signal<[number, number] | null>(null);
  StopForm:FormGroup;
 private readonly alerts = inject(TuiAlertService);
  constructor(private mapService:MapService,private stopService:StopService){
    this.StopForm = new FormGroup({
      address : new FormControl('',[Validators.required]),
      name: new FormControl('',[Validators.required]),
      description : new FormControl('')
    })
  }


getAddress(stop:any) {
  this.stop = stop;
  this.StopForm.controls['address'].setValue(this.stop.address);
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
      this.stopService.addStop(this.stop).subscribe({
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
    this.stop.name = this.StopForm.controls["name"].value;
    this.stop.description = this.StopForm.controls["description"].value;    
    this.stop.status = "active";    
  }
}
