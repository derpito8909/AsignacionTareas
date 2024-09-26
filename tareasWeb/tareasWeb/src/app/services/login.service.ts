import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Credentials } from '../interfaces/credentials';
import { LoginResponse } from '../interfaces/login-response';
import { Router } from '@angular/router';
import { jwtDecode } from 'jwt-decode';

@Injectable({
  providedIn: 'root',
})
export class LoginService {
  private apiUrl = 'http://localhost:5000/api/Auth/login';
  private httpClient = inject(HttpClient);
  private router = inject(Router);

  login(credentials: Credentials): Observable<LoginResponse> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });

    return this.httpClient.post<LoginResponse>(this.apiUrl, credentials, {
      headers,
    });
  }
  // Decodifica el token y retorna su contenido
  private decodeToken(): any {
    const token = this.getToken();
    return token ? jwtDecode<any>(token) : null;
  }

  getToken(): string | null {
    return localStorage.getItem('authToken');
  }

  // Verifica si el usuario ha iniciado sesión
  isLoggedIn(): boolean {
    return !!this.getToken();
  }

  redirectToLogin(): void {
    this.router.navigate(['/login']);
  }

  setToken(token: string): void {
    localStorage.setItem('authToken', token);
  }

  logout(): void {
    localStorage.removeItem('authToken');
    this.router.navigate(['/login']);
  }
}
