// // import { Component } from '@angular/core';
// // import { CommonModule } from '@angular/common';
// // import { FormsModule } from '@angular/forms';
// // import { Router, RouterLink } from '@angular/router';
// // import { AuthService } from '../../../core/services/auth.service';
// // import { NgForm } from '@angular/forms';
// // @Component({
// //   selector: 'app-login',
// //   standalone: true,
// //   templateUrl: './login.component.html',
// //   styleUrls: ['./login.component.scss'],
// //   imports: [CommonModule, FormsModule, RouterLink]
// // })
// // export class LoginComponent {
// //   email = '';
// //   password = '';
// //   loading = false;
// //   error: string | null = null;

// //   constructor(private auth: AuthService, private router: Router) {}

// //   submit(form: NgForm) {
// //     if (form.invalid) return;

// //     this.loading = true;
// //     this.error = null;

// //     this.auth.login({ email: this.email, password: this.password }).subscribe({
// //       next: () => {
// //         this.router.navigateByUrl('/');
// //       },
// //       error: (err) => {
// //         this.error = err?.error || 'Login failed. Please try again.';
// //         this.loading = false;
// //       }
// //     });
// //   }
// // }



// import { Component } from '@angular/core';
// import { CommonModule } from '@angular/common';
// import { FormsModule } from '@angular/forms';
// import { Router, RouterLink } from '@angular/router';
// import { AuthService } from '../../../core/services/auth.service';
// import { NgForm } from '@angular/forms';

// @Component({
//   selector: 'app-login',
//   standalone: true,
//   templateUrl: './login.component.html',
//   styleUrls: ['./login.component.scss'],
//   imports: [CommonModule, FormsModule, RouterLink]
// })
// export class LoginComponent {
//   email = '';
//   password = '';
//   loading = false;
//   error: string | null = null;

//   constructor(private auth: AuthService, private router: Router) {}

//   submit(form: NgForm) {
//     if (form.invalid) return;

//     this.loading = true;
//     this.error = null;

//     this.auth.login({ email: this.email, password: this.password }).subscribe({
//       next: (response) => {
//         // Redirect based on user role
//         this.redirectBasedOnRole(response.user);
//       },
//       error: (err) => {
//         this.error = err?.error || 'Login failed. Please try again.';
//         this.loading = false;
//       }
//     });
//   }

//   private redirectBasedOnRole(user: any) {
//     // Check user role and redirect accordingly
//     if (user.userType === 0 || user.role === 'Admin') {
//       // Admin user - redirect to admin dashboard
//       this.router.navigate(['/admin/dashboard']);
//     } else if (user.userType === 1 || user.role === 'User') {
//       // Regular user - redirect to products page (or user dashboard)
//       this.router.navigate(['/products']);
//     } else {
//       // Fallback - redirect to home
//       this.router.navigate(['/']);
//     }
//   }
// }


import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  imports: [CommonModule, FormsModule, RouterLink]
})
export class LoginComponent {
  email = '';
  password = '';
  loading = false;
  error: string | null = null;

  constructor(private auth: AuthService, private router: Router) {}

  submit() {
    if (!this.email || !this.password) return;

    this.loading = true;
    this.error = null;

    this.auth.login({ email: this.email, password: this.password }).subscribe({
      next: (response: any) => {
        this.loading = false;
        this.redirectBasedOnRole(response.user);
      },
      error: (err: any) => {
        this.loading = false;
        this.error = err?.error?.message || 'Login failed. Please try again.';
      }
    });
  }

  private redirectBasedOnRole(user: any) {
    if (user.userType === 2) {
      this.router.navigate(['/admin/dashboard']);
    } else {
      this.router.navigate(['/products']);
    }
  }
}