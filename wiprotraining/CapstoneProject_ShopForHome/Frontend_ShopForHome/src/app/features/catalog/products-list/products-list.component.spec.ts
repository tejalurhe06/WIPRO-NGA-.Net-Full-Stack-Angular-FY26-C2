import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';   // 👈 import this
import { OrderService } from '../../../core/services/order.service';
@Component({
  selector: 'app-place-order',
  standalone: true,
  imports: [CommonModule],   // 👈 add CommonModule here
  templateUrl: './place-order.component.html',
  styleUrls: ['./place-order.component.scss']
})
export class PlaceOrderComponent {
  loading = false;
  successMessage: string | null = null;
  errorMessage: string | null = null;

  private readonly defaultAddressId = 1;

  constructor(private orderService: OrderService) {}

  placeOrder() {
    this.loading = true;
    this.successMessage = null;
    this.errorMessage = null;

    this.orderService.create(this.defaultAddressId).subscribe({
      next: () => {
        this.successMessage = 'Order placed successfully!';
        this.loading = false;
      },
      error: (err:any) => {
        this.errorMessage = err.error?.message || 'Failed to place order.';
        this.loading = false;
      }
    });
  }
}
