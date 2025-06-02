import { Component, inject, OnInit, signal, TemplateRef, ViewChild } from '@angular/core';
import { RouteMapComponent } from "../../../../shared/components/route-map/route-map.component";
import { MATERIAL_IMPORTS } from '../../../../shared/imports/AngularMateriel.imports';
import { RouteService } from '../../services/route.service';
import { RouteRequest, RouteResponse } from '../../../../shared/types/Dtos/route.dto';
import { ActivatedRoute, Router } from '@angular/router';
import {CdkDragDrop, moveItemInArray} from '@angular/cdk/drag-drop';
import { RouteStopRequest, RouteStopResponse } from '../../../../shared/types/Dtos/routeStop.dto';
import { StopsRouteFormComponent } from "../stops-route-form/stops-route-form.component";
import { TuiAlertService, TuiDialogService } from '@taiga-ui/core';
import { TUI_CONFIRM, TuiConfirmData } from '@taiga-ui/kit';
import { TuiResponsiveDialogService } from '@taiga-ui/addon-mobile';
import { catchError, map, switchMap, throwError } from 'rxjs';
import { StopResponse } from '../../../../shared/types/Dtos/stop.dto';
import { RouteFormComponent } from "../route-form/route-form.component";

@Component({
  selector: 'app-details-route',
  imports: [RouteMapComponent, MATERIAL_IMPORTS, StopsRouteFormComponent, RouteFormComponent],
  templateUrl: './details-route.component.html',
  styleUrl: './details-route.component.css'
})
export class DetailsRouteComponent implements OnInit{
    @ViewChild('routeStopsForm') routeStopsForm!: TemplateRef<any>;
    @ViewChild('routeForm')routeForm!: TemplateRef<any>;
private readonly dialogs = inject(TuiResponsiveDialogService);
  private readonly alerts = inject(TuiAlertService);
  route!:RouteResponse;
  routeStops:RouteStopResponse[] = [];
  points:number[][] = [];
  constructor(private router:Router,private routeService:RouteService,private activatedRoute:ActivatedRoute,private readonly dialogService: TuiDialogService){

  }
  ngOnInit(): void {
    // if(this.activatedRoute.snapshot.params["id"])
    this.loadRoute(11);
    
  }
  displayedColumns: string[] = ['id',"stopOrder",'stop', 'arrivalTime', 'departureTime','actions',];
  

drop(event: CdkDragDrop<any[]>) {
    if (event.previousIndex !== event.currentIndex) {
        const routeStopsCopy = [...this.routeStops];
        moveItemInArray(routeStopsCopy, event.previousIndex, event.currentIndex);
    
        this.routeStops = routeStopsCopy;
       this.routeStops.forEach((stop, index) => {
      stop.stopOrder = index + 1; 
    });
  }

}

editRoute(){
      this.dialogService.open<RouteRequest>(this.routeForm,{
         size: 'auto',
         dismissible: false,
         closeable: true,
         data:this.route
       }).subscribe((result: RouteRequest) => {
         if(result){
             this.routeService.updateRoute(result,this.route.id).subscribe({
               next: (res) => {
                 this.loadRoute(this.route.id);
                //  this.alerts.open("Successfully added");
               },
               error: (err) => {
                //  this.alerts.open('Add failed');
                 console.error('Add failed:', err);
               }
             });
       }
     });
}
deleteRoute() {
          const data: TuiConfirmData = {
            content:
            '',
            yes: 'Delete',
            no: 'Cancel',
            appearance: "error"
        };
    
        let confirm:any =this.dialogs
        .open<boolean>(TUI_CONFIRM, {
            label: 'Are you sure you want to delete this route?',
            size: 'l',
            data,
            dismissible: false,
            closeable: false,
            
        }).pipe(map(res=>{
          if(res){
            return this.routeService.deleteRoute(+this.activatedRoute.snapshot.params["id"]).pipe(
              switchMap(() => this.alerts.open("Successfully deleted")),
              catchError(err=>{
                this.alerts.open('Delete failed');
              return throwError(err);})
            ).subscribe({
              complete:()=>{
                //reditrectHomePage
                // this.router.navigateByUrl("/admin/route");
              }
            });
          }
          return null;
        }))
        .subscribe();
        
      }
   editItem(item: RouteStopResponse) {
    this.dialogService.open<RouteStopRequest>(this.routeStopsForm,{
         size: 'auto',
         dismissible: false,
         closeable: true,
         data:item
       }).subscribe((result: RouteStopRequest) => {
         if(result){
             this.routeService.updateAssignedStop(this.route.id,result).subscribe({
               next: (res) => {
                 this.loadRoute(this.route.id);
                //  this.alerts.open("Successfully added");
               },
               error: (err) => {
                //  this.alerts.open('Add failed');
                 console.error('Add failed:', err);
               }
             });
       }
     });
  
  }

deleteStop(id:number) {
          const data: TuiConfirmData = {
            content:
            '',
            yes: 'Delete',
            no: 'Cancel',
            appearance: "error"
        };
    
        let confirm:any =this.dialogs
        .open<boolean>(TUI_CONFIRM, {
            label: 'Are you sure you want to delete this assigned stop?',
            size: 'l',
            data,
            dismissible: false,
            closeable: false,
            
        }).pipe(map(res=>{
          if(res){
            return this.routeService.deleteAssignedStop(+this.activatedRoute.snapshot.params["id"],id).pipe(
              switchMap(() => this.alerts.open("Successfully deleted")),
              catchError(err=>{
                this.alerts.open('Delete failed');
              return throwError(err);})
            ).subscribe({
              complete:()=>{
                this.loadRoute(this.activatedRoute.snapshot.params["id"]);
              }
            });
          }
          return null;
        }))
        .subscribe();
        
      }


openAddForm(){
   this.dialogService.open<RouteStopRequest>(this.routeStopsForm,{
         size: 'auto',
         dismissible: false,
         closeable: true,
         data:null
       }).subscribe((result: RouteStopRequest) => {
         if(result){
             this.routeService.assignStop(this.route.id,result).subscribe({
               next: (res) => {
                 this.loadRoute(this.route.id);
                //  this.alerts.open("Successfully added");
               },
               error: (err) => {
                //  this.alerts.open('Add failed');
                 console.error('Add failed:', err);
               }
             });
       }
     });
  }

  private getPointsFromRoute(stops:RouteStopResponse[]){
    let points: number[][] = [];
    stops.forEach(stop=>{
      points.push([stop.stop.y,stop.stop.x]);
    })

    return points;
  }

    private loadRoute(id:number){
      this.routeService.getRouteById(id).subscribe({
          next:(data)=>{this.route = data ;this.getRouteStops();},
          error : (err)=>console.error(err),
          
        });
  }
  
private getRouteStops(){
    if(this.route){
      this.routeStops = this.route.routeStops;
      console.log(this.routeStops)
      this.points = this.getPointsFromRoute(this.routeStops);
    }
  }
}
