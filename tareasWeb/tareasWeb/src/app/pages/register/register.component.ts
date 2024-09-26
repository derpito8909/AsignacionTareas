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
import { Users } from '../../interfaces/users';
import { RegisterService } from '../../services/register.service';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    CommonModule,
    MatFormFieldModule,
    MatInputModule,
  ],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css',
})
export class RegisterComponent implements OnInit, OnDestroy {
  private registerService = inject(RegisterService);
  private router = inject(Router);
  private registerSubscription: Subscription | null = null;
  errorMessage: string | null = null;

  // Conectar el formulario con nuestro grupo de controles
  registerForm = new FormGroup({
    nombreCompleto: new FormControl(''),
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required]),
    rol: new FormControl(''),
  });

  ngOnInit(): void {}
  ngOnDestroy(): void {
    this.registerSubscription?.unsubscribe();
  }

  // Obtener las credenciales para iniciar sesión
  private getRegister(): Users | null {
    return this.registerForm.valid ? (this.registerForm.value as Users) : null;
  }

  //metodo para hacer la peticion de login
  onRegister(): void {
    const register = this.getRegister();
    if (register) {
      this.registerSubscription = this.registerService
        .register(register)
        .subscribe({
          next: (res: any) => {
            setTimeout(() => {
              this.handleSuccess(res);
            }, 1000);
          },
          error: (err) => {
            setTimeout(() => {
              this.handleSuccess(err);
            }, 1000);
          },
        });
    }
  }

  private handleSuccess(res: any): void {
    if (res) {
      this.router.navigate(['/login']);
    }
    this.errorMessage = null;
  }

  private handleError(err: any): void {
    this.errorMessage = err.message;
    this.registerForm.reset();
  }
}
