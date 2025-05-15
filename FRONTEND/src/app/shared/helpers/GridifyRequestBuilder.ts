import { GridifyRequest } from "../types/Dtos/gridify.dto";
import { HttpParams } from '@angular/common/http';

export function makeEmployeeRequest(query:GridifyRequest,searchFields:{ field: string; type: 'string' | 'number' }[],filterFields:{field:string; type:'string'|'number'}[]=[] ) {

      const isNumber = !isNaN(Number(query.search));
      const filters: string[] = [];
    
      for (const { field, type }  of searchFields) {
        if (type == "number" && isNumber) {
          filters.push(`${field}=*${query.search}`);
        } else if (type == "string") {
          filters.push(`${field}=*${query.search}`);
        }
      }
    
    let params = new HttpParams()
      .set('page', query.pagination.getPageNumber())
      .set('pageSize', query.pagination.getPageSize());
    if (query.search){
      params = params.set('filter', filters.join('|'));
    }
    
    return params;
}


