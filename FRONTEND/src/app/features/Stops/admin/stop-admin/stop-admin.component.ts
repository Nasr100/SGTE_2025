import { Component, OnInit, signal } from '@angular/core';
import { MapCardComponent } from "../map-card/map-card.component";
import { StopResponse } from '../../../../shared/types/Dtos/stop.dto';
import { StopService } from '../../services/stop.service';
import { Pagination } from '../../../../shared/helpers/pagination';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-stop-admin',
  imports: [MapCardComponent,CommonModule],
  templateUrl: './stop-admin.component.html',
  styleUrl: './stop-admin.component.css'
})
export class StopAdminComponent implements OnInit {
  stops = signal<StopResponse[]>([]);
  count = signal(0);
  search = signal("")
  pagination= signal( new Pagination());

  constructor(private stopService:StopService){
   
  }
  ngOnInit(): void {
      this.stopService.getStops({pagination:this.pagination(),search:this.search()}).subscribe({
        next : (data)=>{
          this.stops.set(data.data)
          this.count.set(data.count)
        },
        complete:()=>{console.log(this.stops())}
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
}
