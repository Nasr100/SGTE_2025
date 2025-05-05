import { ChangeDetectionStrategy, Component, OnInit  } from '@angular/core';
import { RouterLink,RouterModule } from '@angular/router';
import { FORM_IMPORTS } from '../../../shared/imports/Form.imports';
import { SideBarService } from '../../../core/services/side-bar/side-bar.service';

@Component({
  selector: 'app-side-bar',
  imports: [FORM_IMPORTS,RouterModule],
  templateUrl: './side-bar.component.html',
  styleUrl: './side-bar.component.css',
  standalone:true,

})
export class SideBarComponent implements OnInit {
  constructor(private sideBarService:SideBarService){
  }

  ngOnInit(): void {
    this.sideBarService.$isOpen.subscribe(state=>{
      this.sidebarOpen = state;
    })
  }
  sidebarOpen:boolean = false;

}
