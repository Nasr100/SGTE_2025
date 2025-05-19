import { Component, OnInit, signal } from '@angular/core';
import { StopFormComponent } from "../stop-form/stop-form.component";
import { ActivatedRoute } from '@angular/router';
import { StopService } from '../../services/stop.service';
import { StopResponse } from '../../../../shared/types/Dtos/stop.dto';

@Component({
  selector: 'app-stop-detail',
  imports: [StopFormComponent],
  templateUrl: './stop-detail.component.html',
  styleUrl: './stop-detail.component.css'
})
export class StopDetailComponent implements OnInit{
  stopId:number = 0;
  stopSig= signal<StopResponse|null>(null);
  
  constructor(private router:ActivatedRoute,private stopService:StopService){
    
  }

  ngOnInit(): void {
    console.log();
   
        this.loadStop(+this.router.snapshot.params["id"]);


  }

  private loadStop(id:number){
      this.stopService.getStopById(id).subscribe({
          next:(data)=>{this.stopSig.set(data)},
          error : (err)=>console.error(err),
        });
  }


}
