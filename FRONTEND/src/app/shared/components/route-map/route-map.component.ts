import { AfterViewInit, Component, effect, ElementRef, input, ViewChild } from '@angular/core';
import * as L from 'leaflet';
import { MAP_IMPORTS } from '../../imports/Map.imports';
import 'leaflet-routing-machine';

@Component({
  selector: 'app-route-map',
  imports: [MAP_IMPORTS],
  templateUrl: './route-map.component.html',
  styleUrl: './route-map.component.css'
})
export class RouteMapComponent implements AfterViewInit{
    @ViewChild('mapContainer') mapContainer!: ElementRef;

     private map!: L.Map;
      private routingControl!: L.Routing.Control;
      stopPoints = input<number[][]>([]);
constructor(){
  effect(()=>{
    this.initRouting();
  })
}
  ngAfterViewInit(): void {
    this.initMap();
    this.initRouting();
  }

  private initMap(): void {
    this.map = L.map(this.mapContainer.nativeElement);
    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png').addTo(this.map);
  }


 private initRouting() {
    const points = this.stopPoints()?.map(p => L.latLng(p[0], p[p.length-1]));
      if (points.length < 2) {
      console.warn('Need at least 2 points to create a route');
      return;
    }
    if(this.map){
      this.map.setView(points[0], 13);
      if (this.routingControl) {
        this.map.removeControl(this.routingControl);
      }
      this.routingControl = L.Routing.control({
        waypoints: points,
         plan: L.Routing.plan(points, {
        draggableWaypoints: false,
        addWaypoints: false,
      }),
        routeWhileDragging: false,
      show: false, 
      addWaypoints: false,
      fitSelectedRoutes: true,
      lineOptions: {
        styles: [{ color: '#757de8', opacity: 0.7, weight: 10 }],
        extendToWaypoints: false,
        missingRouteTolerance: 0
      },
      }).addTo(this.map);
      this.map.fitBounds(L.latLngBounds(points));
    }
     
      

  }
   
}
