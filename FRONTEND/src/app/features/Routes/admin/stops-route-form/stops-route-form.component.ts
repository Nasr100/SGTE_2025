import { Component, OnInit } from '@angular/core';
import { StopService } from '../../../Stops/services/stop.service';
import { Stop } from '../../../Stops/types/stop.model';
import { GridifyBuilder } from '../../../../shared/helpers/GridifyBuilder';
import { map } from 'rxjs/operators';
import { StopResponse } from '../../../../shared/types/Dtos/stop.dto';
import { CommonModule } from '@angular/common';
import { FormControl, FormGroup, FormsModule, Validators } from '@angular/forms';
import { RouteStopRequest, RouteStopResponse } from '../../../../shared/types/Dtos/routeStop.dto';
import { TuiDialogContext } from '@taiga-ui/core';
import { injectContext } from '@taiga-ui/polymorpheus';
// import { DriverRequest, DriverResponse } from '../../../../shared/types/Dtos/driver.dto';
import { FORM_IMPORTS } from '../../../../shared/imports/Form.imports';
import {arrivalBeforeDepartureValidator} from '../../../../shared/helpers/TimeValidator'
@Component({
  selector: 'app-stops-route-form',
  imports: [CommonModule,FORM_IMPORTS],
  templateUrl: './stops-route-form.component.html',
  styleUrl: './stops-route-form.component.css'
})
export class StopsRouteFormComponent implements OnInit{
    public readonly context = injectContext<TuiDialogContext<RouteStopRequest, RouteStopResponse>>();
  stopRouteForm!:FormGroup;
  routeStopresponse:RouteStopResponse|null = null;
  stops!:Stop[];
  query: GridifyBuilder = new GridifyBuilder();
  stopName:string = "";

  constructor(private stopService:StopService){
    this.stopRouteForm = new FormGroup({
      routeName : new FormControl('',[Validators.required]),
      arrivalTime : new FormControl('',[Validators.required]),
      departureTime : new FormControl('',[Validators.required])
    },{
          validators: arrivalBeforeDepartureValidator() 

    })
    this.routeStopresponse = this.context.data;
    if(this.routeStopresponse){
      this.stopRouteForm.patchValue({
      routeName: this.routeStopresponse.stop.name,
      arrivalTime:this.routeStopresponse.arrivalTime,
      departureTime: this.routeStopresponse.departureTime 
    });
      this.stopName = this.routeStopresponse.stop.name;
      this.checked(this.routeStopresponse.stop.id)
    }
  } 

  ngOnInit(): void {
    this.getStops();
    this.stops?.forEach(stop=>{
      if(this.routeStopresponse?.stop.id == stop.id){
        stop.isChecked = true;
        
      }
    })

  }

private mapToStop(stopResponse: StopResponse[]): Stop[] {
  return stopResponse.map(stop=>{
    return new Stop(stop.id,stop.name,stop.address,stop.description,stop.x,stop.y,stop.status);
  })
}

checked(id:number){
  if(this.stops){
  this.stops.forEach(stop=>{
    if(stop.id == id){
      stop.isChecked = true;
      this.stopName = stop.name
    }
    else{
      stop.isChecked = false;
    }
  })
  }
  
}
  private getStops(){
    this.stopService.getStops(this.query).subscribe({
      next :stop=>{
        this.stops = this.mapToStop(stop.data);
      }
    })
  }

onSubmit(){
  
}


}
