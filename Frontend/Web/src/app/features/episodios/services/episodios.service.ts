import { HttpClient, HttpParams, HttpStatusCode } from '@angular/common/http';
import { inject, Injectable, signal, WritableSignal, Injector, effect } from "@angular/core";
import { finalize } from 'rxjs/operators';
//import { environment } from '../../../../environments/environment';
import { IResultPagination } from '../../../shared/models/ResultPagination';
import { IEpisodio } from '../models/Episodio';
import { IEpisodiosFilter } from '../models/EpisodiosFilter';
import { BffHttpService } from '../../../core/http/bffHttp.service';
import { IServiceResults } from '../../../shared/models/ServiceResult';

@Injectable({
  providedIn: 'root'
})
export class EpisodiosService {
  private readonly injector = inject(Injector);

  // Utilizamos un Signal para almacenar los episodios
  private _episodiosPaginated: WritableSignal<IResultPagination<IEpisodio> | null> = signal(null);

  // Signal para el estado de carga
  public isLoading: WritableSignal<boolean> = signal(false);

  public episodiosFilter: WritableSignal<IEpisodiosFilter | null> = signal(null);

  // Exponemos el estado como un Signal de solo lectura
  public readonly episodiosPaginated = this._episodiosPaginated.asReadonly();

  /** Realiza la llamada del BFF para los episodios usando filtros de busqueda si lo requiere*/
  private getEpisodios(filter: IEpisodiosFilter | null = null): void {
    this.isLoading.set(true); // Activar el indicador de carga

    // Construye los query parameters
    let queryParams = new HttpParams();
    if (filter && filter.PageIndex && filter.PageIndex! > 1)
      queryParams = queryParams.set('PageIndex', filter.PageIndex!);
    if (filter && filter.Id)
      queryParams = queryParams.set('Id', filter.Id!);

    this.bffHttp.get<IResultPagination<IEpisodio>>('episodios/', queryParams)
      .pipe( finalize(() => this.isLoading.set(false)) )
      .subscribe({
        next: (episodios: IServiceResults<IResultPagination<IEpisodio>>) => {
          // Actualizar el Signal con el nuevo valor
          this._episodiosPaginated.set(episodios.data);
        },
        error: (_err) => {
          if (_err.status === HttpStatusCode.NotFound) {
            this._episodiosPaginated.set(null); // No se encontraron episodios
            this.isLoading.set(false);
            return;
          }
          else {
            throw _err;
          }
        }
      });
  }

  constructor(private bffHttp: BffHttpService) {
    effect(() => {
      const newfilter = this.episodiosFilter();
      this.getEpisodios(newfilter);
    }, {
      allowSignalWrites: true,
      injector: this.injector
    });

  }
}
