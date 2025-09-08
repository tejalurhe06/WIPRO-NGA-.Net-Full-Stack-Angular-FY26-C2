


import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { LoginRequest, LoginResponse, User } from '../../shared/models/models';
import { environment } from '../../../environments/environment';
import { StorageUtil } from '../../shared/utils/storage.util';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private base = '/auth';
  private currentUserSubject = new BehaviorSubject<User | null>(null);
  public currentUser$ = this.currentUserSubject.asObservable();
  
  private isLoggedInSubject = new BehaviorSubject<boolean>(!!StorageUtil.getItem('token'));
  isLoggedIn$ = this.isLoggedInSubject.asObservable();

  private roleSubject = new BehaviorSubject<string | null>(StorageUtil.getItem('role'));
  role$ = this.roleSubject.asObservable();

  private firstNameSubject = new BehaviorSubject<string | null>(StorageUtil.getItem('firstName'));
  firstName$ = this.firstNameSubject.asObservable();

  constructor(private http: HttpClient) {
    this.loadUserFromStorage();
  }

  register(dto: any): Observable<any> {
    return this.http.post(`${environment.apiBase}${this.base}/register`, dto);
  }

  login(req: LoginRequest): Observable<LoginResponse> {
    return this.http
      .post<LoginResponse>(`${environment.apiBase}${this.base}/login`, {
        email: req.email,
        password: req.password,
      })
      .pipe(
        tap((res: LoginResponse) => {
          StorageUtil.setItem('token', res.token);
          const role = res.user.userType === 2 ? 'Admin' : 'User';
          StorageUtil.setItem('role', role);
          StorageUtil.setItem('firstName', res.user.firstName);
          StorageUtil.setItem('userId', res.user.userId?.toString() || '');

          const userWithRole: User = {
            ...res.user,
            role: role as 'Admin' | 'User' // Cast to the correct type
          };

          this.isLoggedInSubject.next(true);
          this.roleSubject.next(role);
          this.firstNameSubject.next(res.user.firstName);
          this.currentUserSubject.next(userWithRole);
        })
      );
  }

  logout(): void {
    StorageUtil.clear();
    this.isLoggedInSubject.next(false);
    this.roleSubject.next(null);
    this.firstNameSubject.next(null);
    this.currentUserSubject.next(null);
  }

  getRole(): string | null {
    return StorageUtil.getItem('role');
  }

  isAdmin(): boolean {
    return this.getRole() === 'Admin';
  }

  isUser(): boolean {
    return this.getRole() === 'User';
  }

  getUserId(): number | null {
    const userId = StorageUtil.getItem('userId');
    return userId ? parseInt(userId, 10) : null;
  }

  getFirstName(): string | null {
    return StorageUtil.getItem('firstName');
  }

  isAuthenticated(): boolean {
    return this.isLoggedInSubject.value;
  }

  getCurrentUser(): User | null {
    return this.currentUserSubject.value;
  }

  private loadUserFromStorage(): void {
    const token = StorageUtil.getItem('token');
    const role = StorageUtil.getItem('role');
    const firstName = StorageUtil.getItem('firstName');
    const userId = StorageUtil.getItem('userId');

    if (token) {
      this.isLoggedInSubject.next(true);
      this.roleSubject.next(role);
      this.firstNameSubject.next(firstName);
      
      if (userId) {
        const userType = role === 'Admin' ? 2 : 1;
        const user: User = {
          userId: parseInt(userId, 10),
          firstName: firstName || '',
          email: '',
          userType: userType,
          role: role as 'Admin' | 'User' | null // Cast to correct type
        };
        this.currentUserSubject.next(user);
      }
    }
  }
}