import { CommonModule } from '@angular/common';
import { Component, computed, effect, inject, Injector, OnInit } from '@angular/core';
import { EpisodiosService } from '../services/episodios.service';
import { IEpisodio } from '../models/Episodio';
import { debounceTime, distinctUntilChanged, startWith } from 'rxjs/operators';
import { toSignal } from '@angular/core/rxjs-interop';
import { ReactiveFormsModule, FormBuilder, FormControl } from '@angular/forms';


@Component({
  selector: 'app-episodios',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './episodios.component.html',
  styleUrl: './episodios.component.css'
})
export class EpisodiosComponent implements OnInit {
  private readonly injector = inject(Injector);
  // Fuente de datos completa
  public episodiosService = inject(EpisodiosService);

  // constructor de formularios reactivos
  private formBuilder = inject(FormBuilder);

  // Control de la paginación
  public pageSize: number = 20;
  public currentPage: number = 1;
  public totalIems = computed(() => this.episodiosService.episodiosPaginated()?.count ?? 0);
  public totalPages = computed(() =>
    Math.ceil((this.episodiosService.episodiosPaginated()?.count ?? 0) / this.pageSize)
  );

  // --- Selección de Ítem ---
  public selectedEpisodio: IEpisodio | null = null;

  // Formulario de filtros reactivo
  filterForm = this.formBuilder.group({
    idFilter: new FormControl<string>('', { nonNullable: true }),
  });

  private idFilter$ = toSignal(this.filterForm.controls.idFilter.valueChanges.pipe(
    startWith(this.filterForm.controls.idFilter.value), // Emite el valor inicial
    debounceTime(300), // Espera 300ms después de que el usuario deja de escribir
    distinctUntilChanged() // Solo emite si el valor realmente cambió
  ), {initialValue: ''});

  constructor() {
    // Efecto para actualizar el filtro de episodios cuando cambia el idFilter
    effect(() => {
      const idFilter = this.idFilter$() != '' ? Number(this.idFilter$()): null;

      this.episodiosService.episodiosFilter.set(idFilter ? {Id: idFilter} : {});
    }, {
      allowSignalWrites: true,
      injector: this.injector
    });
  }

  ngOnInit() { }

  /**
   * Establece el episodio seleccionado para mostrarlo en la card de detalle.
   * @param episodio El episodio de la tabla seleccionado.
   */
  selectEpisodio(episodio: IEpisodio): void {
    // Si se hace clic en el mismo ítem, se deselecciona.
    this.selectedEpisodio = this.selectedEpisodio === episodio ? null : episodio;
  }

  /**
   * Actualiza el subconjunto de datos a mostrar según la página actual.
   */
  updatePagination(): void {
    this.episodiosService.episodiosFilter.set({PageIndex: this.currentPage});
    this.selectedEpisodio = null; // Limpiar selección al cambiar de página
  }

  /**
   * Cambia a una nueva página y actualiza los datos.
   * @param newPage El número de la página a la que se desea ir.
   */
  goToPage(newPage: number): void {
    if (newPage >= 1 && newPage <= this.totalPages()) {
      this.currentPage = newPage;
      this.updatePagination();
    }
  }

  /**
   * Genera un array de números de página para mostrar en el paginador.
   */
  get pageNumbers(): number[] {
    return Array(this.totalPages()).fill(0).map((_x, i) => i + 1);
  }
}
