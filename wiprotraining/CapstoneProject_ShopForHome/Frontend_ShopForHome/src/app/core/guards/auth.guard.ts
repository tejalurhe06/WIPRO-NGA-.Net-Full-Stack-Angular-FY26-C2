// auth.guard.ts (update)
import { CanActivateFn } from '@angular/router';
import { inject } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';

export const authGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  const router = inject(Router);
  
  if (authService.isAuthenticated()) {
    // For admin routes, check if user is admin
    if (state.url.startsWith('/admin') && !authService.isAdmin()) {
      router.navigate(['/']);
      return false;
    }
    return true;
  }
  
  router.navigate(['/login']);
  return false;
};