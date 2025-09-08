import { Component, OnInit } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { HttpErrorResponse } from "@angular/common/http";
import { AdminUsersService } from "../../../../core/services/admin-users.service";

@Component({
  selector: "app-admin-users",
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: "./admin-users.component.html",
  styleUrls: ["./admin-users.component.scss"],
})
export class AdminUsersComponent implements OnInit {
  users: any[] = [];
  loading = true;
  error: string | null = null;
  success: string | null = null;

  showForm = false;
  formUser: any = {
    userId: 0,
    firstName: "",
    lastName: "",
    email: "",
    userType: 1,
    isActive: true,
  };

  constructor(private adminUsers: AdminUsersService) {}

  ngOnInit(): void {
    this.loadUsers();
  }

  loadUsers() {
    this.loading = true;
    this.error = null;
    this.adminUsers.list().subscribe({
      next: (res) => {
        this.users = res;
        this.loading = false;
      },
      error: (err: HttpErrorResponse) => {
        this.error = this.extractErrorMessage(err);
        this.loading = false;
        setTimeout(() => this.clearMessages(), 5000);
      },
    });
  }

  editUser(user: any) {
    this.formUser = { ...user };
    this.showForm = true;
  }

  saveUser() {
    this.error = null;
    this.success = null;

    if (!this.formUser.firstName || !this.formUser.lastName || !this.formUser.email) {
      this.error = "All fields are required";
      setTimeout(() => this.clearMessages(), 5000);
      return;
    }

    this.adminUsers.update(this.formUser.userId, this.formUser).subscribe({
      next: () => {
        this.success = "✅ User updated successfully";
        this.loadUsers();
        this.showForm = false;
        setTimeout(() => this.clearMessages(), 5000);
      },
      error: (err: HttpErrorResponse) => {
        this.error = this.extractErrorMessage(err);
        setTimeout(() => this.clearMessages(), 5000);
      },
    });
  }

  deactivateUser(id: number) {
    if (confirm("Are you sure you want to deactivate this user?")) {
      this.adminUsers.delete(id).subscribe({
        next: () => {
          this.success = "✅ User deactivated successfully";
          this.loadUsers();
          setTimeout(() => this.clearMessages(), 5000);
        },
        error: (err: HttpErrorResponse) => {
          this.error = this.extractErrorMessage(err);
          setTimeout(() => this.clearMessages(), 5000);
        },
      });
    }
  }

  cancel() {
    this.showForm = false;
    this.clearMessages();
  }

  private extractErrorMessage(err: HttpErrorResponse): string {
    if (typeof err.error === 'string') {
      return err.error;
    } else if (err.error?.message) {
      return err.error.message;
    } else if (err.error && typeof err.error === 'object') {
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

  clearMessages() {
    this.error = null;
    this.success = null;
  }
}
