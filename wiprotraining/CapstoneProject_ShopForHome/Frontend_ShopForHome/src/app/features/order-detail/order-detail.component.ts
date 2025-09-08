import { Component, OnInit } from "@angular/core";
import { CommonModule } from "@angular/common";
import { ActivatedRoute } from "@angular/router";
import { OrderDetail } from "../../shared/models/models";
import { OrderService } from "../../core/services/order.service";
@Component({
  standalone: true,
  selector: "app-order-detail",
  templateUrl: "./order-detail.component.html",
  styleUrls: ["./order-detail.component.scss"],
  imports: [CommonModule]
})
export class OrderDetailComponent implements OnInit {
  order?: OrderDetail;
  loading = true;

  constructor(private orderService: OrderService, private route: ActivatedRoute) {}

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get("id"));
    this.orderService.get(id).subscribe({
      next: (res) => {
        this.order = res;
        this.loading = false;
      },
      error: () => (this.loading = false)
    });
  }
}
