import { Component, effect, OnInit, signal } from '@angular/core';
import { MapCardComponent } from "../map-card/map-card.component";
import { StopResponse } from '../../../../shared/types/Dtos/stop.dto';
import { StopService } from '../../services/stop.service';
import { Pagination } from '../../../../shared/helpers/pagination';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { GridifyBuilder } from '../../../../shared/helpers/GridifyBuilder';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-stop-admin',
  imports: [MapCardComponent,CommonModule,RouterModule,FormsModule ],
  templateUrl: './stop-admin.component.html',
  styleUrl: './stop-admin.component.css'
})
export class StopAdminComponent implements OnInit {

  stops = signal<StopResponse[]>([]);
  count = signal(0);
  search =signal("");
  pagination= signal( new Pagination());
  gridifyBuilder = signal(new GridifyBuilder())

  constructor(private stopService:StopService){
   effect(()=>{
   
      this.gridifyBuilder().setPagination(this.pagination())
      this.stopService.getStops(this.gridifyBuilder()).subscribe({
        next : (data)=>{
          this.stops.set(data.data)
          this.count.set(data.count)
        },
        complete:()=>{}
      })
   })
  }

  ngOnInit(): void {
    
  }

 Onsearch(event:any){
    const target = event.target as HTMLInputElement;
    this.search.set(target.value)
    this.gridifyBuilder().addCondition("name","=*",this.search());
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

   statusFilter(event:any){
      const target = event.target as HTMLInputElement;
      if(target.value)
      this.gridifyBuilder().addCondition("status","=",target.value);
      this.pagination.update(p =>{
      const newPagination = new Pagination();
        newPagination.setPagination(p);
        newPagination.resetPageNumber();
      return newPagination;
    }) 
   }

   
}
