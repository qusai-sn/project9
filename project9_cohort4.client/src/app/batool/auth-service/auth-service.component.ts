import { Component } from '@angular/core';
import { AuthUrlService } from '../services/auth-service.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-auth-service',
  templateUrl: './auth-service.component.html',
  styleUrls: ['./auth-service.component.css']
})
export class AuthServiceComponent {
  loginMode = true;  // Toggle between login and register forms

  loginData = {
    username: '',
    password: ''
  };

  registerData = {
    username: '',
    email: '',
    fullName: '',
    password: ''
  };

  constructor(
    private authService: AuthUrlService,
    private router: Router
  ) { }

  toggleMode() {
    this.loginMode = !this.loginMode;
  }

  onLoginSubmit() {
    this.authService.login(this.loginData).subscribe({
      next: (response) => {
        console.log('Login successful', response);
        this.router.navigate(['/']);
      },
      error: (error) => {
        console.error('Login failed', error);
        alert(error.error || 'Login failed. Please try again.');
      }
    });
  }

  onRegisterSubmit() {
    this.authService.register(this.registerData).subscribe({
      next: (response) => {
        console.log('Registration successful', response);
        this.router.navigate(['/']);
      },
      error: (error) => {
        console.error('Registration failed', error);
        alert(error.error || 'Registration failed. Please try again.');
      }
    });
  }
}
