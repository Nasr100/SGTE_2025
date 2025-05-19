import { Component, effect, input, OnInit, output } from '@angular/core';
import {  Icon, icon, latLng, marker, polyline, tileLayer, Map, Routing, LatLngExpression, point, Marker, Layer, LeafletMouseEvent } from 'leaflet';
import { MAP_IMPORTS } from '../../imports/Map.imports';
import 'leaflet-routing-machine';
import * as L from 'leaflet';
import { MapService } from '../../services/map/map.service';
import { StopRequest } from '../../types/Dtos/stop.dto';

@Component({
  selector: 'app-map',
  imports: [MAP_IMPORTS],
  templateUrl: './map.component.html',
  styleUrl: './map.component.css'
})
export class MapComponent {
pointCard  = input<[number, number] | null>(null)
 pointSignal = input<[number, number] | null>(null);
  stop = output<StopRequest>();
  map: Map | null = null;
  currentMarker: Marker | null = null;
  
  options = {
    layers: [
      tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
      })
    ],
    center: latLng(34.14420310897081, -6.737371904540356),
    zoom: 12,
  };
  layers: Layer[] = [];

  constructor(private mapService:MapService) {
    effect(() => {
      const coords = this.pointSignal();
      if (this.map && coords) {
        this.updateMarker(coords);
      }
    });
  }

  // onMapReady(map: Map) {
  //   this.map = map;
  //   // Initialize with marker if pointSignal exists
  //   const initialCoords = this.pointSignal();
  //   if (initialCoords) {
  //     this.updateMarker(initialCoords);
  //   }
  // }

  private updateMarker(coords: [number, number]) {
    if (this.currentMarker) {
      this.map?.removeLayer(this.currentMarker);
    }
    
    this.currentMarker = marker([coords[1], coords[0]]);
    this.layers = [this.currentMarker];
    
    this.map?.setView([coords[1], coords[0]], 13);
  }
// centerMarker(number:[number,number]){
// if(this.map)

// this.currentMarker = marker(number).addTo(this.map);
//   this.map?.setView(number, 13);

// }
  
  onMapReady(map: Map) {
    this.map = map;
    let stopReq:StopRequest = {
      name: '',
      address: '',
      x: 0,
      description: '',
      y: 0,
      status: ''
    };
    map.on('click', (e: LeafletMouseEvent) => {
      if(this.currentMarker){
              this.map?.removeLayer(this.currentMarker);
              
      }
      const lat = e.latlng.lat;
      const lng = e.latlng.lng;
     if(this.map){
            this.currentMarker = marker([lat, lng]).addTo(this.map);
                  this.layers = [this.currentMarker];
     }
      

      this.mapService.reverseGeocode(lng, lat).subscribe({
        next: (data) => {
          const address:string = data.features[0]?.properties?.label;
          const city:string = data.features[0]?.properties?.county;
          const newAddresse = address.split(",");
          newAddresse.pop();
          newAddresse.push(city)          
          stopReq.address = newAddresse.join(",");
          stopReq.x = lng;
          stopReq.y = lat;
          this.stop.emit(stopReq);
        },
        error: (err) => console.error(err)
      });
    });
  }
}
