import { HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { IEpisodiosFilter } from "../models/EpisodiosFilter";

@Injectable({
  providedIn: 'root'
})
export class EpisodiosQueryBuilderService {

  public buildQueryParams(filter: IEpisodiosFilter): HttpParams {
    let queryParams = new HttpParams();

    if (!filter.PageIndex && filter.PageIndex! > 1)
      queryParams = queryParams.set('PageIndex', filter.PageIndex!);
    if (filter.Id)
      queryParams = queryParams.set('Id', filter.Id!);
    if (filter.Name)
      queryParams = queryParams.set('Name', filter.Name!);

    return queryParams;
  }
}
