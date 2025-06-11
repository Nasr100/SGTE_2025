import { Component } from '@angular/core';
import { FORM_IMPORTS } from '../../../../shared/imports/Form.imports';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-minitrip-card',
  imports: [FORM_IMPORTS,RouterModule],
  templateUrl: './minitrip-card.component.html',
  styleUrl: './minitrip-card.component.css'
})
export class MinitripCardComponent {

}
