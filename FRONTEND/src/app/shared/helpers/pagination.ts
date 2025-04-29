export class Pagination{
    private pageSize:number;
    private page:number;

    public constructor(){
        this.page = 1;
        this.pageSize = 10;        
    }

    public nexPage(){
         this.page++;
    }
    public prevPage(){
        if(this.page>1){
            this.page--;
        }
    }
    public setPagination(pagination:Pagination){
        Object.assign(this,pagination);
    }
    public getPageNumber(){
        return this.page;
    }
    public getPageSize(){
        return this.pageSize;
    }
    public setPageSize(pageSize:number){
        this.pageSize = pageSize;
    }

    public getCurrent(){
        return this.page*this.pageSize;
    }

    public resetPageNumber(){
        this.page = 1;
    }
} 