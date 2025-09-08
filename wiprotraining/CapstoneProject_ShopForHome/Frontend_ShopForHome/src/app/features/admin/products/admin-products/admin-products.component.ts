import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AdminProductsService } from '../../../../core/services/admin-products.service';

export interface BulkUploadResponse {
  successCount: number;
  errorCount: number;
  errors: string[];
}

@Component({
  selector: 'app-admin-products',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './admin-products.component.html',
})
export class AdminProductsComponent {
  products: any[] = [];
  lowStock: any[] = [];
  stats: any;
  error: string | null = null;
  message: string | null = null;
  file: File | null = null;
  percentageChange: number = 0;

  constructor(private adminProductsService: AdminProductsService) {}

  ngOnInit() {
    this.loadProducts();
    this.loadStats();
    this.loadLowStock();
  }

  loadProducts() {
    this.adminProductsService.list().subscribe({
      next: res => this.products = res,
      error: err => this.error = err.message
    });
  }

  loadLowStock() {
    this.adminProductsService.getLowStock().subscribe({
      next: res => this.lowStock = res,
      error: err => this.error = err.message
    });
  }

  loadStats() {
    this.adminProductsService.getStats().subscribe({
      next: res => this.stats = res,
      error: err => this.error = err.message
    });
  }

  toggleProduct(product: any) {
    this.adminProductsService.toggleStatus(product.productId).subscribe({
      next: () => {
        this.message = 'Product status updated';
        product.isActive = !product.isActive; // ✅ update in table instantly
      },
      error: err => this.error = err.message
    });
  }

  updateStock(product: any, stock: string) {
    const stockValue = +stock;
    this.adminProductsService.updateStock(product.productId, stockValue).subscribe({
      next: () => {
        this.message = 'Stock updated';
        product.stockQuantity = stockValue; // ✅ update in table instantly
      },
      error: (err: any) => this.error = err.message
    });
  }

  updatePrices() {
    this.adminProductsService.updatePrices(this.percentageChange).subscribe({
      next: () => {
        this.message = `Prices updated by ${this.percentageChange}%`;
        this.loadProducts();
      },
      error: err => this.error = err.message
    });
  }

  onFileSelected(event: any) {
    this.file = event.target.files[0];
  }

  bulkUpload() {
    if (!this.file) {
      this.error = 'Please select a file';
      return;
    }

    this.adminProductsService.bulkUpload(this.file).subscribe({
      next: (res: any) => {
        this.message = `Bulk upload done: ${res.successCount} success, ${res.errorCount} errors`;
        if (res.errors?.length) {
          this.error = res.errors.join(', ');
        }
        this.loadProducts();
      },
      error: err => this.error = err.message
    });
  }
}
