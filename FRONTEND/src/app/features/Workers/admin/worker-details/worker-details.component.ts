import { Component, inject, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { WorkerFormComponent } from "../worker-form/worker-form.component";
import { WorkerRequest, WorkerResponse } from '../../../../shared/types/Dtos/worker.dto';
import { ActivatedRoute, Router } from '@angular/router';
import { TuiAlertService, TuiDialogService } from '@taiga-ui/core';
import { WorkerService } from '../../services/worker.service';
import { TuiResponsiveDialogService } from '@taiga-ui/addon-mobile';
import { catchError, map, switchMap, tap, throwError } from 'rxjs';
import { TUI_CONFIRM, TuiConfirmData } from '@taiga-ui/kit';

@Component({
  selector: 'app-worker-details',
  imports: [WorkerFormComponent],
  templateUrl: './worker-details.component.html',
  styleUrl: './worker-details.component.css'
})
export class WorkerDetailsComponent implements OnInit{
  @ViewChild('editForm') EditForm!: TemplateRef<any>;
  workerId!:number;
  worker?:WorkerResponse;

  constructor(private workerService:WorkerService,private route: ActivatedRoute,private readonly dialogService: TuiDialogService,private router:Router){

  }

  ngOnInit(): void {
    this.route.paramMap.subscribe(param=>{
      this.workerId= Number(param.get('id'));
    })

    this.workerService.getWorkerById(this.workerId).subscribe(res=>{
      this.worker = res;
      console.log(res)

    })
  }

  private readonly dialogs = inject(TuiResponsiveDialogService);
  private readonly alerts = inject(TuiAlertService);

  openEditForm() {
      this.dialogService.open<WorkerRequest>(this.EditForm,{
        size: 'page',
        dismissible: true,
        closeable: true,
        data:this.worker
      }).subscribe((result: WorkerRequest) => {
        if(result){
          this.workerService.updateWorker(this.workerId,result).pipe(
            tap((w)=>{
              this.worker = w;
              this.alerts.open("Successfully Updated");
            }),
            catchError(err=>{
              this.alerts.open('Update failed');
            return throwError(err);
          })
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
                return this.workerService.deleteWorker(this.workerId).pipe(
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
