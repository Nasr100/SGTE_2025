import { Component, inject, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { MinitripCardComponent } from "../../../Minitrips/admin/minitrip-card/minitrip-card.component";
import { MinitripRequest, MinitripResponse } from '../../../../shared/types/Dtos/minitrip.dto';
import { TripResponse } from '../../../../shared/types/Dtos/trip.dto';
import { TripService } from '../../services/trip.service';
import { ActivatedRoute, Router } from '@angular/router';
import { MinitripFormComponent } from "../../../Minitrips/admin/minitrip-form/minitrip-form.component";
import { TuiAlertService, TuiDialogService } from '@taiga-ui/core';
import { TuiResponsiveDialogService } from '@taiga-ui/addon-mobile';
import { MinitripService } from '../../../Minitrips/services/minitrip.service';
import { tap } from 'rxjs';

@Component({
  selector: 'app-trip-details',
  imports: [MinitripCardComponent, MinitripFormComponent],
  templateUrl: './trip-details.component.html',
  styleUrl: './trip-details.component.css'
})
export class TripDetailsComponent implements OnInit{
  @ViewChild ('addForm') addForm!:TemplateRef<any>;
  minitrips!:MinitripResponse[];
  trip!:TripResponse;
  tripId!:number;
  constructor(private tripService:TripService,private activatedroute:ActivatedRoute,private route:Router,private readonly dialogService: TuiDialogService,private minitripService:MinitripService){}

  ngOnInit(): void {
    if(this.activatedroute.snapshot.params["id"]){
      this.tripId = this.activatedroute.snapshot.params["id"]
      this.loadTrip(this.tripId);
    }
      
  }

loadTrip(id:number){
    this.tripService.getTripById(id).subscribe({
      next:(trip)=>{this.trip = trip; this.minitrips = trip.miniTrips}
    })
  }
  private readonly alerts = inject(TuiAlertService);
  private readonly dialogs = inject(TuiResponsiveDialogService);
  AddMinitrip(){
     this.dialogService.open<MinitripRequest>(this.addForm,{
              size: 'auto',
              dismissible: false,
              closeable: true,
              data:{tripId:this.tripId,currentTripShift:this.trip.shift}
            }).subscribe((result: MinitripRequest) => {
              if(result){
                this.minitripService.addMinitrip(result).subscribe({
                  next:()=>{
                    this.alerts.open("Successfully added");
                  },
                  error:(err)=>{
                     this.alerts.open('add failed');
                  }
                })
              }
          });
  }

  
}

  
