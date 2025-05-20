import { Component, inject, OnInit, signal } from '@angular/core';
import { StopFormComponent } from "../stop-form/stop-form.component";
import { ActivatedRoute, Router } from '@angular/router';
import { StopService } from '../../services/stop.service';
import { StopResponse } from '../../../../shared/types/Dtos/stop.dto';
import { TuiConfirmData, TUI_CONFIRM } from '@taiga-ui/kit';
import { map, switchMap, catchError, throwError } from 'rxjs';
import { TuiAlertService } from '@taiga-ui/core';
import { TuiResponsiveDialogService } from '@taiga-ui/addon-mobile';

@Component({
  selector: 'app-stop-detail',
  imports: [StopFormComponent],
  templateUrl: './stop-detail.component.html',
  styleUrl: './stop-detail.component.css'
})

export class StopDetailComponent implements OnInit{
  stopId:number = 0;
  stopSig= signal<StopResponse|null>(null);
  private readonly dialogs = inject(TuiResponsiveDialogService);
  private readonly alerts = inject(TuiAlertService);
  constructor(private route:ActivatedRoute,private stopService:StopService,private router:Router){
    
  }

  ngOnInit(): void {
    this.loadStop(+this.route.snapshot.params["id"]);
  }

  // deleteStop(){
    
  //   this.stopService.deleteStop(+this.route.snapshot.params["id"]).subscribe({
  //     complete:()=>{
  //       console.log("stop deleted succefully");
  //     }
  //   })
  // }
onClick() {
      const data: TuiConfirmData = {
        content:
        '',
        yes: 'Delete',
        no: 'Cancel',
        appearance: "error"
    };

    let confirm:any =this.dialogs
    .open<boolean>(TUI_CONFIRM, {
        label: 'Are you sure you want to delete this employee?',
        size: 'l',
        data,
        dismissible: false,
        closeable: false,
        
    }).pipe(map(res=>{
      if(res){
        return this.stopService.deleteStop(+this.route.snapshot.params["id"]).pipe(
          switchMap(() => this.alerts.open("Successfully deleted")),
          catchError(err=>{
            this.alerts.open('Delete failed');
          return throwError(err);})
        ).subscribe({
          complete:()=>{
            this.router.navigate(["/admin/stop"])
          }
        });
      }
      return null;
    }))
    .subscribe();
    
    
  }
  // toggleStopStatus(){
  //   let stop:StopResponse|null = this.stopSig(); 
  //   if(stop?.status == "active"){
  //     stop.status = "inactive"
  //   }else{
  //     if(stop)
  //     stop.status = "active"
  //   }
  //   this.stopSig.set(stop)
  // }

  private loadStop(id:number){
      this.stopService.getStopById(id).subscribe({
          next:(data)=>{this.stopSig.set(data)},
          error : (err)=>console.error(err),
        });
  }


}
