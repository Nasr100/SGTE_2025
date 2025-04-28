import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SideBarService {
  constructor() { }

  private isOpenSubject:BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  $isOpen = this.isOpenSubject.asObservable();
  
  public setSidebarState(state:boolean){
    this.isOpenSubject.next(state);
  }

  public toggle(){
    this.isOpenSubject.next(!this.isOpenSubject.value);
  }
}
