import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "../../../environments/environment";
import { Product } from "../../shared/models/models";

@Injectable({ providedIn: 'root' })
export class AdminProductsService {
  private base = `${environment.apiBase}/admin/adminproducts`;

  constructor(private http: HttpClient) {}

  // ✅ matches GET /all
  list() {
    return this.http.get<Product[]>(`${this.base}/all`);
  }

  // ✅ matches GET /low-stock
  getLowStock() {
    return this.http.get<Product[]>(`${this.base}/low-stock`);
  }

  // ✅ matches POST /bulk-upload
  bulkUpload(file: File) {
    const form = new FormData();
    form.append('file', file);
    return this.http.post(`${this.base}/bulk-upload`, form);
  }

  // ✅ matches POST /{id}/update-stock
  updateStock(productId: number, newStock: number) {
    return this.http.post(`${this.base}/${productId}/update-stock`, newStock);
  }

  // ✅ matches POST /{id}/toggle-status
  toggleStatus(productId: number) {
    return this.http.post(`${this.base}/${productId}/toggle-status`, {});
  }

  // ✅ matches GET /stats
  getStats() {
    return this.http.get(`${this.base}/stats`);
  }

  // ✅ matches POST /update-prices
  updatePrices(percentageChange: number) {
    return this.http.post(`${this.base}/update-prices`, { percentageChange });
  }
}
