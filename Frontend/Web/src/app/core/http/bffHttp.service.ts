import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { API_CONFIG } from '../config/api.config';
// import { IResultPagination } from '../../shared/models/ResultPagination';
import { IServiceResults } from '../../shared/models/ServiceResult';

@Injectable({ providedIn: 'root' })
export class BffHttpService {
  private http: HttpClient = inject(HttpClient);

  get<T>(endpoint: string, params: HttpParams = new HttpParams()) {
    return this.http.get<IServiceResults<T>>(`${API_CONFIG.baseUrl}${endpoint}`, { params });
  }
}
