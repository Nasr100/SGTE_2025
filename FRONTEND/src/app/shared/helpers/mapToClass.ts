
 function mapToClass<T extends object>( classType: new (...args: any[]) => T,response:any):T {
  return Object.assign(new classType(), response);
}

export function mapToClassArray<T extends object>(
  cls: new (...args: any[]) => T,
  responses: any[],
): T[] {
  return responses.map(item => mapToClass(cls, item));
}