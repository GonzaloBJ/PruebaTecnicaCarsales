import { HttpClient, HttpParams, HttpStatusCode } from '@angular/common/http';
import { inject, Injectable, signal, WritableSignal, Injector, effect } from "@angular/core";
import { finalize } from 'rxjs/operators';
//import { environment } from '../../../../environments/environment';
import { IResultPagination } from '../../../shared/models/ResultPagination';
import { IEpisodio } from '../models/Episodio';
import { IEpisodiosFilter } from '../models/EpisodiosFilter';
import { BffHttpService } from '../../../core/http/bffHttp.service';

@Injectable({
  providedIn: 'root'
})
export class EpisodiosService {
  private readonly injector = inject(Injector);
  // private http: HttpClient = inject(HttpClient);
  // private readonly baseUrl: string = environment.BFFUrl;

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

    this.bffHttp.get<IEpisodio>('episodios/', queryParams)
    //this.http.get<IResultPagination<IEpisodio> | string>(`${this.baseUrl}episodios/`, { params: queryParams })
      .pipe(
        // El operador finalize se ejecuta cuando el Observable termina (éxito o error)
        finalize(() => this.isLoading.set(false))
      )
      .subscribe({
        next: (episodios) => {
          // Actualizar el Signal con el nuevo valor
          this._episodiosPaginated.set(episodios as IResultPagination<IEpisodio>);
        },
        error: (_err) => {
          this._episodiosPaginated.set(null); // Limpiar la cesta en caso de error
          this.isLoading.set(false);
          return;
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
