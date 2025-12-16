export interface IResultPagination<T> {
  Success: boolean;
  Data: T | null;
  Code: number;
  ErrorMessage: string | null;
}
