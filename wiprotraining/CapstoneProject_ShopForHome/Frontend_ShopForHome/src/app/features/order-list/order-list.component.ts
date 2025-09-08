import { Component, OnInit } from "@angular/core";
import { CommonModule } from "@angular/common";
import { RouterLink } from "@angular/router";
import { OrderService } from "../../core/services/order.service";
import { OrderSummary } from "../../shared/models/models";
@Component({
  standalone: true,
  selector: "app-orders-list",
  templateUrl: "./order-list.component.html",
  styleUrls: ["./order-list.component.scss"],
  imports: [CommonModule, RouterLink]
})
export class OrderListComponent implements OnInit {
  orders: OrderSummary[] = [];
  loading = true;

  constructor(private orderService: OrderService) {}

  ngOnInit(): void {
    this.orderService.list().subscribe({
      next: (res) => {
        this.orders = res;
        this.loading = false;
      },
      error: () => (this.loading = false)
    });
  }
}
