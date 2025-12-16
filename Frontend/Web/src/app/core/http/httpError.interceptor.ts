import { HttpErrorResponse, HttpInterceptorFn } from "@angular/common/http";
import { inject } from "@angular/core";
import { Router } from "@angular/router";
import { throwError } from "rxjs";
import { catchError } from "rxjs/operators";

export const httpErrorInterceptor: HttpInterceptorFn = (req, next) => {
  const router = inject(Router);

  return next(req).pipe(
    catchError((error: HttpErrorResponse) => {

      let message = 'Error inesperado';

      if (error.status === 404) {
        return throwError(() => error);
      }

      switch (error.status) {
        case 0:
          message = 'No se pudo conectar con el servidor';
          break;
        case 401:
          message = 'No autorizado';
          break;
        case 403:
          message = 'Acceso denegado';
          break;
        case 500:
          message = 'Error interno del servidor';
          break;
      }

      router.navigate(['/error'], {
        queryParams: {
          status: error.status,
          message
        }
      });

      return throwError(() => error);
    })
  );
};
