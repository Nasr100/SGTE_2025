import { Component, effect, inject, signal, TemplateRef, ViewChild } from '@angular/core';
import { BusRequest, BusResponse } from '../../../../shared/types/Dtos/bus.dto';
import { Pagination } from '../../../../shared/helpers/pagination';
import { BusService } from '../../services/bus.service';
import { TuiAlertService, TuiDialogService } from '@taiga-ui/core';
import { FORM_IMPORTS } from '../../../../shared/imports/Form.imports';
import { TUI_IMPOTS } from '../../../../shared/imports/Tui.imports';
import {RouterModule } from '@angular/router';
import { BusFormComponent } from "../bus-form/bus-form.component";

@Component({
  selector: 'app-bus-admin',
  imports: [TUI_IMPOTS, FORM_IMPORTS, RouterModule, BusFormComponent],
  templateUrl: './bus-admin.component.html',
  styleUrl: './bus-admin.component.css'
})
export class BusAdminComponent {
  @ViewChild('addForm') addForm!: TemplateRef<any>;

  buses = signal<BusResponse[]>([]);
  search=signal('');
  filter=signal('');
  pagination= signal( new Pagination());
  count = signal(0);

  constructor(private busService:BusService,private readonly dialogService: TuiDialogService){
     effect(()=>{
      let paginationValue = this.pagination();
      let searchValue = this.search();
      this.busService.getBuses({pagination:paginationValue,search:searchValue}).subscribe(data=>{
        this.buses.set(data.data);
        this.count.set(data.count);
      })
  
    })  
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
   this.dialogService.open<BusRequest>(this.addForm,{
         size: 'page',
         dismissible: true,
         closeable: true,
         data:null
       }).subscribe((result: BusRequest) => {
         if(result){
             this.busService.addBus(result).subscribe({
               next: (res) => {
                 this.buses.update((current) => [...current, res]);
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
}
