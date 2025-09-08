import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ProductService } from '../../../core/services/product.service';
import { Product } from '../../../shared/models/models';
import { HttpErrorResponse } from '@angular/common/http';
import { environment } from '../../../../environments/environment';
import { CartService } from '../../../core/services/cart.service';

@Component({
  selector: 'app-product-detail',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.scss']
})
export class ProductDetailComponent implements OnInit {
  product?: Product;
  loading = true;
  error: string | null = null;
  imageBase = environment.imageBase;

  constructor(
    private route: ActivatedRoute,
    private productService: ProductService,
    private cart: CartService
  ) {}

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.productService.getById(id).subscribe({
      next: (res: Product) => {
        this.product = res;
        this.loading = false;
      },
      error: (err: HttpErrorResponse) => {
        console.error('Error fetching product detail', err.message);
        this.error = 'Failed to load product details';
        this.loading = false;
      }
    });
  }

  getImageUrl(imagePath: string): string {
  if (!imagePath) return 'assets/no-image.png';
  return `${environment.imageBase}/Images/${imagePath}`;
}


  addToCart(productId: number) {
    this.cart.add(productId, 1).subscribe({
      next: () => alert("Added to cart!"),
      error: (err) => console.error("Add to cart failed", err)
    });
  }
}
