import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Users } from '../interfaces/users';

@Injectable({
  providedIn: 'root',
})
export class RegisterService {
  private baseUrl = 'http://localhost:5000/api/Auth/register';
  private httpClient = inject(HttpClient);

  register(users: Users): Observable<Users> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.httpClient.post<any>(`${this.baseUrl}`, users, { headers });
  }
}
