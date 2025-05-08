import { Component, inject, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { DriverAddFormComponent } from "../driver-add-form/driver-add-form.component";
import { DriverRequest, DriverResponse } from '../../../../shared/types/Dtos/driver.dto';
import { DriverService } from '../../service/driver.service';
import { ActivatedRoute, Router } from '@angular/router';
import { TuiAlertService, TuiDialogService } from '@taiga-ui/core';
import { TuiResponsiveDialogService } from '@taiga-ui/addon-mobile';
import { catchError, map, switchMap, throwError } from 'rxjs';
import { TUI_CONFIRM, TuiConfirmData } from '@taiga-ui/kit';

@Component({
  selector: 'app-driver-details',
  imports: [DriverAddFormComponent],
  templateUrl: './driver-details.component.html',
  styleUrl: './driver-details.component.css'
})
export class DriverDetailsComponent implements OnInit{
  @ViewChild('editForm') EditForm!: TemplateRef<any>;
  DriverId!:number;
  Driver?:DriverResponse;
  constructor(private driverService:DriverService,private route: ActivatedRoute,private readonly dialogService: TuiDialogService,private router:Router){

  }

  ngOnInit(): void {
    this.route.paramMap.subscribe(param=>{
      this.DriverId= Number(param.get('id'));
    })

    this.driverService.getDriverbyId(this.DriverId).subscribe(res=>{
      this.Driver = res;
      console.log(res)

    })
  }
  private readonly dialogs = inject(TuiResponsiveDialogService);
  private readonly alerts = inject(TuiAlertService);

  openEditForm() {
      this.dialogService.open<DriverRequest>(this.EditForm,{
        size: 'page',
        dismissible: true,
        closeable: true,
        data:this.Driver
      }).subscribe((result: DriverRequest) => {
        if(result){
          this.driverService.updateDriver(this.DriverId,result).pipe(
            switchMap((res) => this.alerts.open("Successfully Updated")),
            catchError(err=>{
              this.alerts.open('Update failed');
            return throwError(err);})
          ).subscribe()
        }
    });
    }

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
            return this.driverService.deleteDriver(this.DriverId).pipe(
              switchMap((res) => this.alerts.open("Successfully deleted")),
              catchError(err=>{
                this.alerts.open('Delete failed');
              return throwError(err);})
            ).subscribe();
          }
          return null;
        }))
        .subscribe(res=>{
          this.router.navigate(["/Admin/driver"]);
        });
        
        
      }
    
}
