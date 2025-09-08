import { Component, OnInit } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { HttpErrorResponse } from "@angular/common/http";
import { AdminOrdersService } from "../../../../core/services/admin-orders.service";

@Component({
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: "./admin-orders.component.html",
  styleUrls: ["./admin-orders.component.scss"],
})
export class AdminOrdersComponent implements OnInit {
  orders: any[] = [];
  error: string | null = null;
  success: string | null = null;
  loading = true;

  constructor(private ao: AdminOrdersService) {}

  ngOnInit() {
    this.loadOrders();
  }

  loadOrders() {
    this.loading = true;
    this.error = null;
    this.success = null;

    this.ao.list().subscribe({
      next: (r) => {
        this.orders = r.map((x: any) => ({
          ...x,
          newStatus: x.orderStatus, // ✅ backend uses OrderStatus
        }));
        this.loading = false;
      },
      error: (err: HttpErrorResponse) => {
        this.error = err.message || "Failed to load orders";
        this.loading = false;
      },
    });
  }

  update(o: any) {
    this.success = null;
    this.error = null;

    this.ao.updateStatus(o.orderId, o.newStatus).subscribe({
      next: () => {
        // ✅ update local immediately
        o.orderStatus = o.newStatus;
        this.success = `✅ Order #${o.orderId} updated to "${o.newStatus}"`;
      },
      error: (err: HttpErrorResponse) => {
        this.error = err.message || "Failed to update order status";
      },
    });
  }
}
