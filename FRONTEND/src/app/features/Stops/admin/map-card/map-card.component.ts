import { Component, input ,} from '@angular/core';
import { MapComponent } from "../../../../shared/components/map/map.component";
import { MAP_IMPORTS } from '../../../../shared/imports/Map.imports';
import { StopResponse } from '../../../../shared/types/Dtos/stop.dto';
import { FORM_IMPORTS } from '../../../../shared/imports/Form.imports';
import { LatLng, LatLngExpression } from 'leaflet';

@Component({
  selector: 'app-map-card',
  imports: [MapComponent,MAP_IMPORTS,FORM_IMPORTS],
  templateUrl: './map-card.component.html',
  styleUrl: './map-card.component.css'
})
export class MapCardComponent {
  stop = input<StopResponse|null>();
  constructor(){

  }

  makePoint(): [number, number] | null {
  const stop = this.stop?.();
  if (stop && stop.x !== undefined && stop.y !== undefined) {
    return [stop.x, stop.y];
  }
  return null;
}

}
