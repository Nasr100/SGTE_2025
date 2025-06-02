import { Component, computed, input } from '@angular/core';
import { RouteMapComponent } from "../../../../shared/components/route-map/route-map.component";
import { RouteResponse } from '../../../../shared/types/Dtos/route.dto';
import { RouterModule } from '@angular/router';
import { FORM_IMPORTS } from '../../../../shared/imports/Form.imports';
import { RouteStopResponse } from '../../../../shared/types/Dtos/routeStop.dto';

@Component({
  selector: 'app-route-card',
  imports: [RouteMapComponent,RouterModule,FORM_IMPORTS],
  templateUrl: './route-card.component.html',
  styleUrl: './route-card.component.css'
})
export class RouteCardComponent {
  route=input<RouteResponse>();

   stopPoints = computed(() => {
    const stops = this.route()?.routeStops;
    return stops?.map(rs => [rs.stop.y, rs.stop.x]) || [];
  });
}
