import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { environment } from "../../../environments/environment";
import { Product } from "../../shared/models/models";

@Injectable({ providedIn: 'root' })
export class WishlistService {
  private base = '/wishlists';

  constructor(private http: HttpClient) {}

  list() {
    return this.http.get<Product[]>(`${environment.apiBase}${this.base}`);
  }

  // productId goes in URL (no request body)
  add(productId: number) {
    return this.http.post(`${environment.apiBase}${this.base}/${productId}`, {});
  }

  remove(productId: number) {
    return this.http.delete(`${environment.apiBase}${this.base}/${productId}`);
  }

  has(productId: number) {
    return this.http.get<boolean>(`${environment.apiBase}${this.base}/check/${productId}`);
  }
}
