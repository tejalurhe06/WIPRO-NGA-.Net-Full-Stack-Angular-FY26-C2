import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, Router } from '@angular/router';
import { CartService } from '../../core/services/cart.service';
import { ProductService } from '../../core/services/product.service';
import { environment } from '../../../environments/environment';
import { OrderService } from '../../core/services/order.service';
import { Address } from '../../shared/models/models';
import { AddressService } from '../../core/services/address.service';
@Component({
  selector: 'app-cart',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss'],
})
export class CartComponent implements OnInit {
  items: any[] = [];
  total = 0;
  imageBase = environment.imageBase;
  addresses: Address[] = [];
selectedAddressId: number | null = null;

  constructor(
    private cartService: CartService,
    private productService: ProductService,
    private orderService: OrderService,
    private addressService: AddressService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadCart();
    this.loadAddresses();
  }

  loadCart() {
    this.cartService.list().subscribe((cartItems) => {
      this.items = [];
      cartItems.forEach((item: any) => {
        this.productService.getById(item.productId).subscribe((product) => {
          this.items.push({ ...item, product });
          this.calculateTotal();
        });
      });
    });
  }

  getImageUrl(imagePath: string): string {
    if (!imagePath) {
      return 'assets/no-image.png';
    }
    return `${this.imageBase}/Images/${imagePath}`;
  }

  calculateTotal() {
    this.total = this.items.reduce(
      (sum, i) => sum + (i.product?.price || 0) * i.quantity,
      0
    );
  }

  inc(item: any) {
    this.cartService.update(item.cartItemId, item.quantity + 1).subscribe(() => {
      item.quantity++;
      this.calculateTotal();
    });
  }

  dec(item: any) {
    if (item.quantity > 1) {
      this.cartService.update(item.cartItemId, item.quantity - 1).subscribe(() => {
        item.quantity--;
        this.calculateTotal();
      });
    }
  }

  remove(item: any) {
    this.cartService.remove(item.cartItemId).subscribe(() => {
      this.items = this.items.filter((i) => i.cartItemId !== item.cartItemId);
      this.calculateTotal();
    });
  }

  continueShopping() {
    this.router.navigate(['/products']);
  }

  loadAddresses() {
  this.addressService.list().subscribe((res:any) => {
    this.addresses = res;
    if (res.length > 0) {
      this.selectedAddressId = res[0].addressId; // default
    }
  });
}

placeOrder() {
  if (!this.selectedAddressId) {
    alert('Please select a shipping address');
    return;
  }

  this.orderService.create({ addressId: this.selectedAddressId }).subscribe({
    next: (res: any) => {
      this.router.navigate(['/orders', res.orderId]);
    },
    error: (err) => {
      console.error('Order creation failed', err);
      alert(err.error || 'Failed to place order');
    }
  });
}


}
