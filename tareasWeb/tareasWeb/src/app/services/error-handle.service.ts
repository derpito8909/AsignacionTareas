import { Injectable, inject } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { LoginService } from './login.service';

@Injectable({
  providedIn: 'root',
})
export class ErrorHandleService {
  private router = inject(Router);
  private navegation = inject(ActivatedRoute);
  private loginService = inject(LoginService);

  handleHttpError(error: HttpErrorResponse): Observable<never> {
    let errorMessage = '';

    if (error.error instanceof ErrorEvent) {
      // Error del lado del cliente o de red
      errorMessage = `${error.error.message}`;
    } else if (!navigator.onLine) {
      errorMessage =
        'No tienes conexión a Internet. Revisa tu conexión e intenta nuevamente.';
    } else {
      // Error del lado del servidor
      switch (error.status) {
        case 401:
          errorMessage = `${error.error.message}`;
          break;
        case 403:
          errorMessage = `${error.error.message}`;
          break;
        case 400:
          errorMessage = `${error.error.message}`;
          break;
        case 404:
          errorMessage = `${error.error.message}`;
          break;
        case 500:
          errorMessage = `${error.error.message}`;
          break;
        default:
          errorMessage = 'Ocurrió un error inesperado. Inténtalo más tarde.';
      }
    }
    return throwError(() => new Error(errorMessage));
  }
}
