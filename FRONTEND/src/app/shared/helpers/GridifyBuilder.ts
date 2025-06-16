import { Pagination } from "./pagination";

export class GridifyBuilder{
     pagination:Pagination|null;
    private conditions: string[][] ;
    private orderBy: string[][];

    constructor(){
        this.pagination = new Pagination();
        this.conditions = [];
        this.orderBy = [];
    }
    addOrderBy(col:string,direction:string){
       let tab = [col,direction];
        this.orderBy.push(tab);
    }
    addCondition(col:string,op:string,val:string){
        let tab = [col,op,val];
        if(this.conditions.length)
            this.and();
        this.conditions.push(tab);
        return this;
    }

    and(){
        this.conditions.push([","]);
         return this;
    }
    or(){
        this.conditions.push(["|"]);
         return this;
    }
    getCondtions(){
        return this.conditions;
    }
    getOrderBy(){
        return this.orderBy;
    }
    build(){
         let Filetrquery = ""
        let OrderByquery = ""
        if(this.conditions || this.orderBy){
                    Filetrquery = this.conditions.map(t=>t.join("")).join("");
                 OrderByquery = this.orderBy.map(t=>t.join("")).join(" ");
        }else{
            return null
        }
        
       

        this.conditions = [];
        this.orderBy = [];

        return {
            filter:Filetrquery,
            orderBy:OrderByquery
        };
    }
    setPagination(pagination:Pagination|null){
        this.pagination = pagination;
    }
}