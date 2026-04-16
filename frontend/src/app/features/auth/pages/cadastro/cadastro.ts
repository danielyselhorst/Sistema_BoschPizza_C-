import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { Auth } from '../../../../core/services/auth';
import { CommonModule } from '@angular/common';
 
@Component({
  selector: 'app-cadastro',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './cadastro.html',
  styleUrl: './cadastro.css',
})
export class Cadastro {
 
  private fb = inject(FormBuilder);
  private auth = inject(Auth);
  private router = inject(Router);
 
  loading = false;
 
  form = this.fb.group({
    username: ['', [Validators.required, Validators.minLength(3)]],
    password: ['', [Validators.required, Validators.minLength(3)]],
  });
 
  save() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }
 
    this.loading = true;
 
    this.auth.register(this.form.value).subscribe({
      next: () => {
        alert('Usuário criado com sucesso!');
        this.router.navigate(['/login']);
      },
      error: (err) => {
        console.error(err);
        alert('Erro ao cadastrar');
        this.loading = false;
      }
    });
  }
 
  irParaLogin() {
  this.router.navigate(['/login']);
}
}