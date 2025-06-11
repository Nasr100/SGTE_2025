import { Component, effect, signal, TemplateRef, ViewChild } from '@angular/core';
import { Pagination } from '../../../../shared/helpers/pagination';
import { TuiDialogService } from '@taiga-ui/core';
import { FORM_IMPORTS } from '../../../../shared/imports/Form.imports';
import { RouterModule } from '@angular/router';
import { TripFormComponent } from "../trip-form/trip-form.component";
import { TripRequest } from '../../../../shared/types/Dtos/trip.dto';

@Component({
  selector: 'app-home-trips',
  imports: [FORM_IMPORTS, RouterModule, TripFormComponent],
  templateUrl: './home-trips.component.html',
  styleUrl: './home-trips.component.css'
})
export class HomeTripsComponent {
  @ViewChild('addTripForm') addForm!: TemplateRef<any>;

search=signal('');
  filter=signal('');
  pagination= signal( new Pagination());
  count = signal(0);

    constructor(private readonly dialogService: TuiDialogService){
     effect(()=>{
      let paginationValue = this.pagination();
      let searchValue = this.search();
      // this.busService.getBuses({pagination:paginationValue,search:searchValue}).subscribe(data=>{
      //   this.buses.set(data.data);
      //   this.count.set(data.count);
      // })
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

   openAddTripForm(){
      this.dialogService.open<TripRequest>(this.addForm,{
            size: 'auto',
            dismissible: false,
            closeable: true,
            data:null
          }).subscribe((result: TripRequest) => {
            if(result){
                // this.busService.addBus(result).subscribe({
                //   next: (res) => {
                //     this.buses.update((current) => [...current, res]);
                //     this.alerts.open("Successfully added");
                //   },
                //   error: (err) => {
                //     this.alerts.open('Add failed');
                //     console.error('Add failed:', err);
                //   }
                // });
          }
        });
     }
}
