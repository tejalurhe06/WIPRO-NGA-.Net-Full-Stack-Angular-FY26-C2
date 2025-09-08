import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { Product } from '../../models/models';
import { environment } from '../../../../environments/environment';

@Component({
  selector: 'app-product-card',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './product-card.component.html',
  styleUrls: ['./product-card.component.scss']
})
export class ProductCardComponent {
  @Input() product!: Product;

  // set this true when rendering inside Wishlist so the heart acts as "remove"
  @Input() inWishlist = false;

  @Output() addToCart = new EventEmitter<number>();
  @Output() removeFromWishlist = new EventEmitter<number>();
  @Output() addToWishlist = new EventEmitter<number>(); // optional (for product list)

  img(url?: string) {
    if (!url) return '';
    // DB has `/images/SmartSpeaker.jpg`
    return url.startsWith('http') ? url : `${environment.imageBase}${url}`;
  }

  hideImage(event: Event) {
  const el = event.target as HTMLImageElement;
  if (el) {
    el.style.display = 'none';
  }
}

  stockLabel(qty: number) {
    return qty === 0 ? 'Out of Stock' : qty < 10 ? 'Low Stock' : 'In Stock';
  }

  badgeClass(qty: number) {
    return qty === 0 ? 'bg-danger' : qty < 10 ? 'bg-warning text-dark' : 'bg-success';
  }

  onAddToCart() {
    this.addToCart.emit(this.product.productId);
  }

  onWishlistClick() {
    if (this.inWishlist) {
      this.removeFromWishlist.emit(this.product.productId);
    } else {
      this.addToWishlist.emit(this.product.productId);
    }
  }
}
