import { Component, inject, OnInit, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  ReactiveFormsModule,
  FormGroup,
  Validators,
  FormControl,
} from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { Credentials } from '../../interfaces/credentials';
import { LoginService } from '../../services/login.service';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { LoginResponse } from '../../interfaces/login-response';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    CommonModule,
    MatFormFieldModule,
    MatInputModule,
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class LoginComponent implements OnInit, OnDestroy {
  private loginService = inject(LoginService);
  private router = inject(Router);
  private loginSubscription: Subscription | null = null;
  errorMessage: string | null = null;

  // Conectar el formulario con nuestro grupo de controles
  credentialForm = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required]),
  });

  ngOnInit(): void {}
  ngOnDestroy(): void {
    this.loginSubscription?.unsubscribe();
  }

  // Obtener las credenciales para iniciar sesión
  private getCredentials(): Credentials | null {
    return this.credentialForm.valid
      ? (this.credentialForm.value as Credentials)
      : null;
  }

  //metodo para hacer la peticion de login
  onLogin(): void {
    const credentials = this.getCredentials();
    if (credentials) {
      this.loginSubscription = this.loginService.login(credentials).subscribe({
        next: (res: LoginResponse) => {
          setTimeout(() => {
            this.handleSuccess(res);
          }, 1000);
        },
        error: (err) => {
          setTimeout(() => {
            this.handleError(err);
          }, 1000);
        },
      });
    }
  }

  private handleSuccess(res: LoginResponse): void {
    if (res?.token) {
      this.loginService.setToken(res.token);
      this.router.navigate(['/inicio']);
    }
    this.errorMessage = null;
  }

  private handleError(err: any): void {
    this.errorMessage = err.message;
    this.credentialForm.reset();
  }
}
