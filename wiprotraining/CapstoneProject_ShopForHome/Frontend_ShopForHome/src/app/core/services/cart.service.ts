import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "../../../environments/environment";
import { CartItem } from "../../shared/models/models";
import { Observable } from 'rxjs';
@Injectable({ providedIn: 'root' })
export class CartService {
    private base = '/carts';

    constructor(private http: HttpClient) { }

    list() {
        return this.http.get<CartItem[]>(`${environment.apiBase}${this.base}`);
    }


    add(productId: number, quantity = 1) {
        return this.http.post(`${environment.apiBase}${this.base}`, { productId, quantity });
    }

    update(cartItemId: number, quantity: number) {
        return this.http.put(`${environment.apiBase}/carts/${cartItemId}`, { quantity });
    }

    remove(cartItemId: number) {
        return this.http.delete(`${environment.apiBase}/carts/${cartItemId}`);
    }


    clear() {
        return this.http.delete(`${environment.apiBase}${this.base}`);
    }

    total() {
        return this.http.get<number>(`${environment.apiBase}${this.base}/total`);
    }
}
