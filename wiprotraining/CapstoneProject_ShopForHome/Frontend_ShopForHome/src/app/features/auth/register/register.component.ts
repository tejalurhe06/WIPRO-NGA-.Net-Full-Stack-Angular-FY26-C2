// import { Component } from '@angular/core';
// import { CommonModule } from '@angular/common';
// import { FormsModule } from '@angular/forms';
// import { Router } from '@angular/router';
// import { AuthService } from '../../../core/services/auth.service';

// @Component({
//   selector: 'app-register',
//   standalone: true,
//   templateUrl: './register.component.html',
//   styleUrls: ['./register.component.scss'],
//   imports: [CommonModule, FormsModule]
// })
// export class RegisterComponent {
//   firstName = '';
//   lastName = '';
//   email = '';
//   password = '';
//   userType = 1; // default User
//   loading = false;
//   error: string | null = null;

//   constructor(private auth: AuthService, private router: Router) {}

//   submit() {
//     this.loading = true;
//     this.error = null;

//     const dto = {
//       firstName: this.firstName,
//       lastName: this.lastName,
//       email: this.email,
//       password: this.password,
//       userType: this.userType
//     };

//     this.auth.register(dto).subscribe({
//       next: (res) => {
//         alert('Registration successful! Please login.');
//         this.router.navigateByUrl('/login');
//       },
//       error: (err) => {
//         this.error = err?.error || 'Registration failed. Try again.';
//         this.loading = false;
//       }
//     });
//   }
// }



import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-register',
  standalone: true,
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
  imports: [CommonModule, FormsModule] // REMOVED RouterLink
})
export class RegisterComponent {
  firstName = '';
  lastName = '';
  email = '';
  password = '';
  confirmPassword = '';
  loading = false;
  error: string | null = null;

  constructor(private auth: AuthService, private router: Router) {}

  submit() {
    if (this.password !== this.confirmPassword) {
      this.error = 'Passwords do not match';
      return;
    }

    if (this.password.length < 6) {
      this.error = 'Password must be at least 6 characters long';
      return;
    }

    this.loading = true;
    this.error = null;

    const dto = {
      firstName: this.firstName,
      lastName: this.lastName,
      email: this.email,
      password: this.password
    };

    this.auth.register(dto).subscribe({
      next: (res: any) => {
        this.loading = false;
        this.router.navigate(['/login']);
      },
      error: (err: any) => {
        this.loading = false;
        this.error = err?.error?.message || 'Registration failed. Please try again.';
      }
    });
  }
}