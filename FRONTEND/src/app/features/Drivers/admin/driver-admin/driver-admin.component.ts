import { Component,effect,inject, OnInit, signal, TemplateRef, ViewChild} from '@angular/core';
import { DriverRequest, DriverResponse } from '../../../../shared/types/Dtos/driver.dto';
import { Pagination } from '../../../../shared/helpers/pagination';
import { TuiDialogService, TuiDialogContext, TuiDialogSize, TuiAlertService } from '@taiga-ui/core';
import type { PolymorpheusContent } from '@taiga-ui/polymorpheus';
import { FORM_IMPORTS } from '../../../../shared/imports/Form.imports';
import { TUI_IMPOTS } from '../../../../shared/imports/Tui.imports';
import { DriverService } from '../../service/driver.service';
import { DriverAddFormComponent } from "../driver-add-form/driver-add-form.component";
import {RouterModule } from '@angular/router';


@Component({
  selector: 'app-driver-admin',
  imports: [TUI_IMPOTS, FORM_IMPORTS, DriverAddFormComponent,RouterModule],
  templateUrl: './driver-admin.component.html',
  styleUrl: './driver-admin.component.css'
})

export class DriverAdminComponent {
  @ViewChild('addForm') addForm!: TemplateRef<any>;

  drivers = signal<DriverResponse[]>([]);
  search=signal('');
  filter=signal('');
  pagination= signal( new Pagination());
  count = signal(0);

  constructor(private driverService:DriverService,private readonly dialogService: TuiDialogService){
    effect(()=>{
      let paginationValue = this.pagination();
      let searchValue = this.search();
      this.driverService.getDrivers({pagination:paginationValue,search:searchValue}).subscribe(data=>{
        this.drivers.set(data.data);
        this.count.set(data.count);
      })
  
    })  
  }

  OnFilter(event:Event){

  }

  Onsearch(event:Event){
    const target = event.target as HTMLInputElement;
    this.search.set(target.value);
    this.pagination.update(p =>{
      const newPagination = new Pagination();
        newPagination.setPagination(p);
        newPagination.resetPageNumber();
      return newPagination;
    }) 
  }

  nextPage(){
    this.pagination.update(p=>{
      const newPagination = new Pagination();
      newPagination.setPagination(p);
      if(p.getCurrent()<this.count()){
        newPagination.nexPage();
      }
      return newPagination;
    })
   }
  
   prevPage(){
    this.pagination.update(p =>{
      const newPagination = new Pagination();
        newPagination.setPagination(p);
        newPagination.prevPage();
      return newPagination;
    })
   }
   private readonly dialogs = inject(TuiDialogService);
      private readonly alerts = inject(TuiAlertService);

 openAddForm(){
   this.dialogService.open<DriverRequest>(this.addForm,{
         size: 'page',
         dismissible: true,
         closeable: true,
         data:null
       }).subscribe((result: DriverRequest) => {
         if(result){
             
             this.driverService.addDriver(result).subscribe({
               next: (res) => {
                 this.drivers.update((current) => [...current, res]);
                 this.alerts.open("Successfully added");
               },
               error: (err) => {
                 this.alerts.open('Add failed');
                 console.error('Add failed:', err);
               }
             });
       }
     });
  }
//  protected onClick(
//      content: PolymorpheusContent<TuiDialogContext>,
//      size: TuiDialogSize,
//  ): void {
//      this.dialogs
//          .open(content, {
//              size,
//          })
//          .subscribe();
//  }
  
}