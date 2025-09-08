import { Injectable } from "@angular/core";
import { environment } from "../../../environments/environment";
import { HttpClient } from "@angular/common/http";
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';

@Injectable({ providedIn: "root" })
export class AdminCouponsService {
  private base = "/admin/admincoupons";

  constructor(private http: HttpClient) {}

  list() {
    return this.http.get<any[]>(`${environment.apiBase}${this.base}`).pipe(
      catchError(error => {
        console.error('List coupons error:', error);
        const errorMessage = error.error?.message || error.message || 'Failed to load coupons';
        return throwError(() => new Error(errorMessage));
      })
    );
  }

  create(c: any) {
    // Map frontend properties to backend DTO structure
    const createDto = {
      Code: c.code.toUpperCase(), // Ensure uppercase
      Description: c.description || '',
      DiscountType: parseInt(c.discountType), // Now 1=Percentage, 2=Fixed
      DiscountValue: parseFloat(c.discount),
      MinimumAmount: c.minimumAmount || 0,
      ExpiresAt: c.validTo ? new Date(c.validTo).toISOString() : null
    };

    console.log('Sending create request:', createDto);

    return this.http.post(`${environment.apiBase}${this.base}`, createDto).pipe(
      catchError(error => {
        console.error('Create coupon error details:', error);
        // Extract error message from backend response
        const errorMessage = error.error || error.message || 'Failed to create coupon';
        return throwError(() => new Error(typeof errorMessage === 'string' ? errorMessage : JSON.stringify(errorMessage)));
      })
    );
  }

  assign(couponCode: string, userId: number) {
    const assignDto = {
      CouponCode: couponCode.toUpperCase(), // Ensure uppercase
      UserId: userId
    };

    console.log('Sending assign request:', assignDto);

    return this.http.post(`${environment.apiBase}${this.base}/assign`, assignDto).pipe(
      catchError(error => {
        console.error('Assign coupon error details:', error);
        // Extract error message from backend response
        const errorMessage = error.error || error.message || 'Failed to assign coupon';
        return throwError(() => new Error(typeof errorMessage === 'string' ? errorMessage : JSON.stringify(errorMessage)));
      })
    );
  }
}