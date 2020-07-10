export class Page {
  constructor(public pageSize = 10, public pageNumber = 1, public sortFields = 'a.id asc', public keyword = '', public total = 0) {}
}

export class PageView<T> {
  pageSize: number;
  pageNumber: number;
  sortFields: string;
  keyword: string;
  total: number;
  datas: T[];
}

export class AnyResult {
  code: StateCode;
  message: string;
  data: any;
}

export class Result<T> {
  code: StateCode;
  message: string;
  data: T;
}

export enum StateCode {
  Ok = 1,
  Fail = 2,
}
