export interface IResultPagination<T> {
  pageIndex: number;
  pageSize: number;
  count: number;
  data: T[];
}
