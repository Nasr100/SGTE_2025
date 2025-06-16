import { Component,input } from '@angular/core';
import { FORM_IMPORTS } from '../../../../shared/imports/Form.imports';
import { RouterModule } from '@angular/router';
import { MinitripResponse } from '../../../../shared/types/Dtos/minitrip.dto';

@Component({
  selector: 'app-minitrip-card',
  imports: [FORM_IMPORTS,RouterModule],
  templateUrl: './minitrip-card.component.html',
  styleUrl: './minitrip-card.component.css'
})
export class MinitripCardComponent {
  minitrip = input<MinitripResponse|null>();
  constructor(){}
}
