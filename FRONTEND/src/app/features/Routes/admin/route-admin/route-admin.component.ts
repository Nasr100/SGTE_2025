import { Component, effect, signal} from '@angular/core';
import { RouteResponse } from '../../../../shared/types/Dtos/route.dto';
import { GridifyBuilder } from '../../../../shared/helpers/GridifyBuilder';
import { Pagination } from '../../../../shared/helpers/pagination';
import { RouteService } from '../../services/route.service';
import { FORM_IMPORTS } from '../../../../shared/imports/Form.imports';
import { RouterModule } from '@angular/router';
import { RouteCardComponent } from "../route-card/route-card.component";

@Component({
  selector: 'app-route-admin',
  imports: [FORM_IMPORTS, RouterModule, RouteCardComponent],
  templateUrl: './route-admin.component.html',
  styleUrl: './route-admin.component.css'
})

export class RouteAdminComponent {
  routes = signal<RouteResponse[]>([]);
    count = signal(0);
  search =signal("");
  pagination= signal( new Pagination());
  gridifyBuilder = signal(new GridifyBuilder())

  constructor(private routeService:RouteService){
   effect(()=>{
   
      this.gridifyBuilder().setPagination(this.pagination())
      this.routeService.getRoutes(this.gridifyBuilder()).subscribe({
        next : (data)=>{
          this.routes.set(data.data)
          this.count.set(data.count)
        },
        complete:()=>{}
      })
   })
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
      this.gridifyBuilder().addCondition("isActive","=",target.value);
      this.pagination.update(p =>{
      const newPagination = new Pagination();
        newPagination.setPagination(p);
        newPagination.resetPageNumber();
      return newPagination;
    }) 
   }
}
