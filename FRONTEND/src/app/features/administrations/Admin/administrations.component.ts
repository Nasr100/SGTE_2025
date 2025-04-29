import { Component,effect,inject, OnInit, signal} from '@angular/core';
import { TUI_IMPOTS } from '../../../shared/imports/Tui.imports';
import { FORM_IMPORTS } from '../../../shared/imports/Form.imports';
import { AdministrationService } from '../services/administration.service';
import { Administrations } from '../types/administration.model';
import { GridifyRequest, GridifyResponse } from '../../../shared/types/Dtos/gridify.dto';
import { administrations } from '../administration.routes';
import { Pagination } from '../../../shared/helpers/pagination';
import { map, single } from 'rxjs';
import { AdministartionResponse } from '../../../shared/types/Dtos/administration.dto';

@Component({
  selector: 'app-administrations',
  imports: [TUI_IMPOTS,FORM_IMPORTS],
  templateUrl: './administrations.component.html',
  styleUrl: './administrations.component.css'
})
export class AdministrationsComponent implements OnInit{
  administration = signal<AdministartionResponse[]>([]);
  search=signal('') ;
  pagination= signal( new Pagination());
  count = signal(0);

 constructor(private administrationService:AdministrationService){
  effect(()=>{
    let paginationValue = this.pagination();
    let searchValue = this.search();
    this.administrationService.getAdministrations({pagination:paginationValue,search:searchValue}).subscribe(data=>{
      this.administration.set(data.data);
      console.log(data.data);

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

}


