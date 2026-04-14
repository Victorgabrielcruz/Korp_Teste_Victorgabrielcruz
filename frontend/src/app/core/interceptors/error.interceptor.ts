import { HttpInterceptorFn, HttpErrorResponse } from '@angular/common/http';
import { throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  return next(req).pipe(
    catchError((error: HttpErrorResponse) => {
      const message = error.error?.message ?? error.message ?? 'Erro desconhecido';
      console.error(`[HTTP Error] ${error.status}: ${message}`);
      return throwError(() => new Error(message));
    })
  );
};
