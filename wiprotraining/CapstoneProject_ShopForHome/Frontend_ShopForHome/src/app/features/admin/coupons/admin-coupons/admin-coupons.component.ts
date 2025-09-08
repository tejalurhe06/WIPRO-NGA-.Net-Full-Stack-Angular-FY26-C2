import { Component, OnInit } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { AdminCouponsService } from "../../../../core/services/admin-coupons.service";
import { HttpErrorResponse } from "@angular/common/http";
import { environment } from "../../../../../environments/environment";

@Component({
  selector: "app-admin-coupons",
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: "./admin-coupons.component.html",
  styleUrls: ["./admin-coupons.component.scss"],
})
export class AdminCouponsComponent implements OnInit {
  coupons: any[] = [];
  today = new Date();
  form: any = {
    code: "",
    description: "",
    discountType: "1", // Default to Percentage (1)
    discount: 10,
    minimumAmount: 0,
    validTo: ""
  };
  assignCode = "";
  assignUserId?: number;

  loading = true;
  error: string | null = null;
  success: string | null = null;

  constructor(private ac: AdminCouponsService) {}

  ngOnInit() {
    this.loadCoupons();
  }

  loadCoupons() {
    this.loading = true;
    this.error = null;
    
    this.ac.list().subscribe({
      next: (r) => {
        this.coupons = r;
        this.loading = false;
      },
      error: (err: HttpErrorResponse) => {
        this.error = this.extractErrorMessage(err);
        this.loading = false;
        setTimeout(() => this.clearMessages(), 5000);
      }
    });
  }

  create() {
    this.error = null;
    this.success = null;

    // Basic validation
    if (!this.form.code) {
      this.error = "Coupon code is required";
      return;
    }

    // Validate coupon code format
    if (!/^[A-Z0-9-]+$/.test(this.form.code)) {
      this.error = "Coupon code must contain only uppercase letters, numbers, and dashes";
      return;
    }

    if (!this.form.discount || this.form.discount <= 0) {
      this.error = "Discount must be greater than 0";
      return;
    }

    // Validate expiry date if provided
    if (this.form.validTo) {
      const expiryDate = new Date(this.form.validTo);
      if (expiryDate <= new Date()) {
        this.error = "Expiry date must be in the future";
        return;
      }
    }

    this.ac.create(this.form).subscribe({
      next: () => {
        this.success = "✅ Coupon created successfully";
        this.resetForm();
        this.loadCoupons();
        setTimeout(() => this.clearMessages(), 5000);
      },
      error: (err: HttpErrorResponse) => {
        this.error = this.extractErrorMessage(err);
        setTimeout(() => this.clearMessages(), 5000);
      }
    });
  }

  assign() {
    this.error = null;
    this.success = null;

    if (!this.assignCode) {
      this.error = "Coupon code is required";
      return;
    }

    if (!this.assignUserId || this.assignUserId <= 0) {
      this.error = "Valid User ID is required";
      return;
    }

    this.ac.assign(this.assignCode, this.assignUserId).subscribe({
      next: () => {
        this.success = "🎟️ Coupon assigned successfully";
        this.assignCode = "";
        this.assignUserId = undefined;
        setTimeout(() => this.clearMessages(), 5000);
      },
      error: (err: HttpErrorResponse) => {
        this.error = this.extractErrorMessage(err);
        setTimeout(() => this.clearMessages(), 5000);
      }
    });
  }

  // Helper method to extract error message from different response formats
  private extractErrorMessage(err: HttpErrorResponse): string {
    console.log('Error response:', err);
    
    if (typeof err.error === 'string') {
      return err.error;
    } else if (err.error?.message) {
      return err.error.message;
    } else if (err.error && typeof err.error === 'object') {
      // Handle validation errors from backend
      if (err.error.errors) {
        const errorMessages = [];
        for (const key in err.error.errors) {
          if (err.error.errors[key]) {
            errorMessages.push(...err.error.errors[key]);
          }
        }
        return errorMessages.join(', ');
      }
      return JSON.stringify(err.error);
    } else if (err.message) {
      return err.message;
    } else {
      return 'An unknown error occurred';
    }
  }

  // Make this method public so it can be called from the template
  clearMessages() {
    this.error = null;
    this.success = null;
  }

  private resetForm() {
    this.form = {
      code: "",
      description: "",
      discountType: "1",
      discount: 10,
      minimumAmount: 0,
      validTo: ""
    };
  }

  // Helper method to format discount display
  getDiscountDisplay(coupon: any): string {
    if (coupon.discountType === 1 || coupon.discountType === 'Percentage') {
      return `${coupon.discountValue}%`;
    } else {
      return `$${coupon.discountValue}`;
    }
  }

  // Helper method to check if coupon is expired
  isExpired(coupon: any): boolean {
    if (!coupon.expiresAt) return false;
    return new Date(coupon.expiresAt) < new Date();
  }
}