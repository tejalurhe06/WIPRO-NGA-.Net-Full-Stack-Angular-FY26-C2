import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterLink, RouterModule } from '@angular/router';
import { ProductService } from '../../../core/services/product.service';
import { Product } from '../../../shared/models/models';
import { HttpErrorResponse } from '@angular/common/http';
import { environment } from '../../../../environments/environment';
import { CartService } from '../../../core/services/cart.service';
import { WishlistService } from '../../../core/services/wishlist.service';
@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './products-list.component.html',
  styleUrls: ['./products-list.component.scss']
})
export class ProductListComponent implements OnInit {
  products: Product[] = [];
  loading = true;
  error: string | null = null;
  imageBase = environment.imageBase;

  constructor(private productService: ProductService,private cart: CartService,private router: Router,private wishlist: WishlistService) {}

  ngOnInit(): void {
    this.productService.getAll().subscribe({
      next: (res: Product[]) => {
        this.products = res;
        this.loading = false;
      },
      error: (err: HttpErrorResponse) => {
        console.error('Error fetching products', err.message);
        this.error = 'Failed to load products';
        this.loading = false;
      }
    });
  }

  getImageUrl(imagePath: string): string {
  if (!imagePath) {
    return 'assets/no-image.png'; // fallback if no image in DB
  }
  return `${this.imageBase}/Images/${imagePath}`;
}

  addToCart(productId: number) {
  this.cart.add(productId, 1).subscribe({
    next: () => alert("Added to cart!"),
    error: (err) => console.error("Add to cart failed", err)
  });
}

// addToWishlist(product: any) {
//   alert(`✅ ${product.name} has been added to your Wishlist!`);
//   // Later you can integrate real wishlist API here
// }

addToWishlist(productId: number, productName: string) { // ✅ New method
    this.wishlist.add(productId).subscribe({
      next: () => alert(`${productName} added to wishlist!`),
      error: (err) => console.error("Add to wishlist failed", err)
    });
  }
  
  
}
  
