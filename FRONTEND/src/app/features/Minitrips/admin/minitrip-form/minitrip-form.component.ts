import { Component, OnInit } from '@angular/core';
import { BusService } from '../../../buses/services/bus.service';
import { EmployeeService } from '../../../employee/services/employee.service';
import { injectContext } from '@taiga-ui/polymorpheus';
import { MinitripDialogData} from '../../../../shared/types/models/MinitripDialogData.model';
import { TuiDialogContext } from '@taiga-ui/core';
import { FORM_IMPORTS } from '../../../../shared/imports/Form.imports';
import { Bus } from '../../../buses/types/bus.model';
import { map } from 'rxjs';
import { mapToClassArray } from '../../../../shared/helpers/mapToClass';
import { Employee } from '../../../employee/types/employee.model';
import { MinitripRequest } from '../../../../shared/types/Dtos/minitrip.dto';

@Component({
  selector: 'app-minitrip-form',
  imports: [FORM_IMPORTS],
  templateUrl: './minitrip-form.component.html',
  styleUrl: './minitrip-form.component.css'
})
export class MinitripFormComponent implements OnInit{
  buses!:Bus[];
  drivers!:Employee[];
  filteredBuses!:Bus[];
  filteredDrivers!:Employee[];
  tripId:number;
  shift:string;
      public readonly context = injectContext<TuiDialogContext<MinitripRequest, MinitripDialogData>>();
busInput: any;
driverInput: any;
MinitripName: any;
  minitripRequest!:MinitripRequest ;
  constructor(private busService:BusService,private employeeService:EmployeeService){
    this.tripId = this.context.data.tripId;
    this.shift = this.context.data.currentTripShift;
  }
  ngOnInit(): void {
    this.loadBuses();
    this.loadDrivers();
    
  }

  loadBuses(){
    this.busService.getAvailableBuses(this.tripId,this.shift).pipe(
      map((response) => mapToClassArray(Bus,response))
    ).subscribe({
      next:(data)=>{
        this.buses = data;
        this.filteredBuses = this.buses;
      }
    })
  }

  loadDrivers(){
    this.employeeService.getEmployeeByRole("driver").pipe(
      map((response) => mapToClassArray(Employee,response))
    ).subscribe({
      next:(data)=>{
        this.drivers = data;
        this.filteredDrivers = this.drivers
      }
    })
  }

  onBusclick(id:number){
    this.filteredBuses.forEach(b=>{
      if(b.id == id){
        b.isChecked = true;
        this.minitripRequest.busId = id;
      }else{
        b.isChecked = false;
      }
    })
  }

  onDriverclick(id:number){
    this.filteredDrivers.forEach(b=>{
      if(b.id == id){
        b.isChecked = true;
        this.minitripRequest.driverId = id;
        
      }else{
        b.isChecked = false;
      }
    })
  }

  driverSearch(){
    if (!this.driverInput.trim()) {
      this.filteredDrivers = this.drivers;

    }else{
            const searchLower = this.driverInput.toLowerCase();
             this.filteredDrivers = this.drivers.filter(d =>
        d.firstName.toLowerCase().includes(searchLower) ||
        (d.lastName.toLowerCase().includes(searchLower))
      );

    }
  }
  busSearch(){
    if (!this.busInput.trim()) {
      this.filteredBuses = this.buses;

    }else{
            const searchLower = this.busInput.toLowerCase();
             this.filteredBuses = this.buses.filter(b =>
        b.number.toLowerCase().includes(searchLower));

    }
  }
  
  onSubmit(){
    if(this.minitripRequest.busId !=0 && this.minitripRequest.driverId !=0){
      this.minitripRequest.tripId= this.tripId;
      this.minitripRequest.name = this.MinitripName;
      this.context.completeWith(this.minitripRequest);
    }
        

  }
}
