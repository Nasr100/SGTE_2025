import { Component } from '@angular/core';
import { SideBarService } from '../../../core/services/side-bar/side-bar.service';

@Component({
  selector: 'app-header',
  imports: [],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css',
  standalone:true
})
export class HeaderComponent {
  constructor(private SideBarService:SideBarService){}

  toggleSideBar(){
    this.SideBarService.toggle();
  }
}
