import { Component, OnInit } from "@angular/core";
import { CommonModule } from "@angular/common";
import { Product } from "../../shared/models/models";
import { WishlistService } from "../../core/services/wishlist.service";
import { CartService } from "../../core/services/cart.service";
import { HttpErrorResponse } from "@angular/common/http";
import { environment } from "../../../environments/environment";
import { Router, RouterLink } from "@angular/router";

@Component({
  standalone: true,
  imports: [CommonModule],   
  templateUrl: "./wishlist.component.html",
  styleUrls: ["./wishlist.component.scss"]
})
export class WishlistComponent implements OnInit {
  products: Product[] = [];
  loading = true;
  error: string | null = null;
  info: string | null = null;
  imageBase = environment.imageBase;

  constructor(
    private wl: WishlistService,
    private cart: CartService,
    private router: Router
  ) {}

  ngOnInit() {
    this.load();
  }

  load() {
    this.loading = true;
    this.error = null;
    this.info = null;

    this.wl.list().subscribe({
      next: (r: Product[]) => {
        this.products = r;
        this.loading = false;
      },
      error: (err: HttpErrorResponse) => {
        this.error = err.message || "Failed to load wishlist";
        this.loading = false;
      }
    });
  }

  getImageUrl(imagePath: string): string {
    if (!imagePath) {
      return "assets/no-image.png";
    }
    return `${this.imageBase}/Images/${imagePath}`;
  }

  addToCart(productId: number) {
    this.info = null;
    this.cart.add(productId, 1).subscribe({
      next: () => {
        this.info = "Added to cart ✅";
        // 👉 If you want to redirect to Cart page:
         this.router.navigate(['/cart']);
      },
      error: (err: HttpErrorResponse) => {
        this.error = err.message || "Failed to add to cart";
      }
    });
  }

  browseProducts() {
    this.router.navigate(["/products"]);
  }

  remove(productId: number) {
    this.info = null;
    this.wl.remove(productId).subscribe({
      next: () => {
        this.info = "Removed from wishlist";
        this.load(); // refresh list
      },
      error: (err: HttpErrorResponse) => {
        this.error = err.message || "Failed to remove";
      }
    });
  }
}
