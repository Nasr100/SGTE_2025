import { Component, effect, inject, signal, TemplateRef, ViewChild } from '@angular/core';
import { WorkerRequest, WorkerResponse } from '../../../../shared/types/Dtos/worker.dto';
import { Pagination } from '../../../../shared/helpers/pagination';
import { WorkerService } from '../../services/worker.service';
import { TuiAlertService, TuiDialogService } from '@taiga-ui/core';
import { WorkerFormComponent } from "../worker-form/worker-form.component";
import { FORM_IMPORTS } from '../../../../shared/imports/Form.imports';
import { TUI_IMPOTS } from '../../../../shared/imports/Tui.imports';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-admin-worker',
  imports: [WorkerFormComponent,TUI_IMPOTS, FORM_IMPORTS,RouterModule],
  templateUrl: './admin-worker.component.html',
  styleUrl: './admin-worker.component.css'
})
export class AdminWorkerComponent {
  @ViewChild('addForm') addForm!: TemplateRef<any>;

  workers = signal<WorkerResponse[]>([]);
  search=signal('');
  filter=signal('');
  pagination= signal( new Pagination());
  count = signal(0);
constructor(private workerService:WorkerService,private readonly dialogService: TuiDialogService){
  effect(()=>{
    let paginationValue = this.pagination();
    let searchValue = this.search();
    this.workerService.getWorkers({pagination:paginationValue,search:searchValue}).subscribe(data=>{
      this.workers.set(data.data);
      console.log(data.data);
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
         this.dialogService.open<WorkerRequest>(this.addForm,{
               size: 'page',
               dismissible: true,
               closeable: true,
               data:null
             }).subscribe((result: WorkerRequest) => {
               if(result){
                   
                   this.workerService.addWorker(result).subscribe({
                     next: (res) => {
                       this.workers.update((current) => [...current, res]);
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
