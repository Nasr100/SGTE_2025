import { Component,inject} from '@angular/core';
import { TUI_IMPOTS } from '../../shared/imports/Tui.imports';
import { FORM_IMPORTS } from '../../shared/imports/Form.imports';

@Component({
  selector: 'app-administrations',
  imports: [TUI_IMPOTS,FORM_IMPORTS],
  templateUrl: './administrations.component.html',
  styleUrl: './administrations.component.css'
})
export class AdministrationsComponent {
 
}
