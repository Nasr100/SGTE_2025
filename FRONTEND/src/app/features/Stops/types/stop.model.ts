

export class Stop{
    public id:number;
   public name:string;
  public  address:string;
    public description:string;
   public x:number;
   public y:number;
   public status:string;
   public isChecked:boolean = false
    constructor(id:number,
    name:string,
    address:string,
    description:string,
    x:number,
    y:number,
    status:string){
        this.address = address;
        this.description = description
        this.id = id
        this.name = name
        this.x = x
        this.y = y
        this.status = status
    }

  
}