import { Component,effect,inject, OnInit, signal, TemplateRef, ViewChild} from '@angular/core';
import { TUI_IMPOTS } from '../../../shared/imports/Tui.imports';
import { FORM_IMPORTS } from '../../../shared/imports/Form.imports';
import { AdministrationService } from '../services/administration.service';
import { Pagination } from '../../../shared/helpers/pagination';
import { AdministartionRequest, AdministartionResponse } from '../../../shared/types/Dtos/administration.dto';
import {TuiAlertService, TuiDialogService} from '@taiga-ui/core';
import { AddFormComponent } from "./add-form/add-form.component";
import {RouterModule } from '@angular/router';
import { catchError, switchMap, throwError } from 'rxjs';

@Component({
  selector: 'app-administrations',
  imports: [TUI_IMPOTS, FORM_IMPORTS, AddFormComponent,RouterModule],
  templateUrl: './administrations.component.html',
  styleUrl: './administrations.component.css'
})
export class AdministrationsComponent implements OnInit{
  @ViewChild('addForm') addForm!: TemplateRef<any>;

  administration = signal<AdministartionResponse[]>([]);
  search=signal('') ;
  pagination= signal( new Pagination());
  count = signal(0);

 constructor(private administrationService:AdministrationService,private readonly dialogService: TuiDialogService){

  effect(()=>{
    let paginationValue = this.pagination();
    let searchValue = this.search();
    this.administrationService.getAdministrations({pagination:paginationValue,search:searchValue}).subscribe(data=>{
      this.administration.set(data.data);
      this.count.set(data.count);
    })

  })  
  
 }

  ngOnInit(): void {
   
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

   private readonly alerts = inject(TuiAlertService);

 openAddForm(){
  this.dialogService.open<AdministartionRequest>(this.addForm,{
        size: 'page',
        dismissible: true,
        closeable: true,
        data:null
      }).subscribe((result: AdministartionRequest) => {
        if(result){
            
            this.administrationService.addAdministration(result).subscribe({
              next: (res) => {
                this.administration.update((current) => [...current, res]);
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


