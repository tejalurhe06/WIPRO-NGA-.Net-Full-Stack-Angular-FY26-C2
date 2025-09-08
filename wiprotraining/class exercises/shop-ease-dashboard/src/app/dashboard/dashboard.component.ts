import { Component } from '@angular/core';
import { Product ,PRODUCTS } from '../app.config';
import { CommonModule } from '@angular/common';
import { ProductDetailsComponent } from '../product-details/product-details.component';
@Component({
  selector: 'app-dashboard',
  standalone:true,
  imports: [CommonModule,ProductDetailsComponent],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent {

  products: Product[] = PRODUCTS;
  selectedProduct: Product | null = null;
  feedbacks: { productId: number; comment: string; rating: number }[] = [];

  // When product clicked
  selectProduct(product: Product) {
    this.selectedProduct = product;
  }

  // Receive feedback from child
  receiveFeedback(feedback: { comment: string; rating: number }) {
    if (this.selectedProduct) {
      const existingIndex = this.feedbacks.findIndex(f => f.productId === this.selectedProduct!.id);
      if (existingIndex > -1) {
        this.feedbacks[existingIndex] = { productId: this.selectedProduct.id, ...feedback };
      } else {
        this.feedbacks.push({ productId: this.selectedProduct.id, ...feedback });
      }
    }
  }

  // Get feedback for a product
  getFeedback(productId: number) {
    return this.feedbacks.find(f => f.productId === productId);
  }
}
