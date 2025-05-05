import { Component, inject, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { TuiAlertService, TuiDialogService } from '@taiga-ui/core';
import {TuiResponsiveDialogService} from '@taiga-ui/addon-mobile';
import { TUI_CONFIRM, TuiConfirmData } from '@taiga-ui/kit';
import { catchError, map,switchMap,throwError } from 'rxjs';
import { AdministrationService } from '../../services/administration.service';
import { ROUTER_IMPORT } from '../../../../shared/imports/Router.imports';
import { ActivatedRoute } from '@angular/router';
import { AddFormComponent } from "../add-form/add-form.component";
import { AdministartionResponse } from '../../../../shared/types/Dtos/administration.dto';

@Component({
  selector: 'app-administartion-detail',
  imports: [ROUTER_IMPORT, AddFormComponent],
  templateUrl: './administartion-detail.component.html',
  styleUrl: './administartion-detail.component.css'
})
export class AdministartionDetailComponent implements OnInit{
  @ViewChild('editForm') EditForm!: TemplateRef<any>;
  AdministartionId!:number;
  AdministrationMember!:any;
  constructor(private administrationService:AdministrationService,private route: ActivatedRoute,private readonly dialogService: TuiDialogService){

  }
  
  ngOnInit(): void {
    this.route.paramMap.subscribe(param=>{
      this.AdministartionId = Number( param.get('id'));
    })
    
    this.administrationService.getAdministrationById(3).subscribe(res=>{
      this.AdministrationMember = res;
      // console.log(this.AdministrationMember)
    });

  }
  private readonly dialogs = inject(TuiResponsiveDialogService);
  private readonly alerts = inject(TuiAlertService);

  openEditForm() {
    this.dialogService.open(this.EditForm,{
      size: 'page',
      dismissible: true,
      closeable: true,
      data:this.AdministrationMember
    }).subscribe(result => {
        console.log(result)
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
        return this.administrationService.deleteAdministration(this.AdministartionId).pipe(
          switchMap((res) => this.alerts.open("Successfully deleted")),
          catchError(err=>{
            this.alerts.open('Delete failed');
          return throwError(err);})
        ).subscribe();
      }
      return null;
    }))
    .subscribe();
    

  }


}
