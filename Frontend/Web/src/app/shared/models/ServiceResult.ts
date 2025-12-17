export interface IServiceResults<T> {
  success: boolean;
  data: T | null;
  code: number;
  errorMessage: string | null;
}
