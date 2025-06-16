export class Bus{
    public id:number;
    public number:string;
    public busStatus:string;
    public capacity:number;
    public isChecked:boolean ;
    constructor(id:number,number:string,busStatus:string,capacity:number){
        this.id = id;
        this.busStatus = busStatus;
        this.number = number;
        this.capacity = capacity;
        this.isChecked = false;
    }

    toggleIschecked(){
        this.isChecked = !this.isChecked;
    }
    
}