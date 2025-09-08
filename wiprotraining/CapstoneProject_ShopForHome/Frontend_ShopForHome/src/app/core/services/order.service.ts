import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { environment } from "../../../environments/environment";
import { OrderSummary, OrderDetail, CreateOrderRequest } from "../../shared/models/models";

@Injectable({ providedIn: "root" })
export class OrderService {
  private base = `${environment.apiBase}/orders`;

  constructor(private http: HttpClient) {}

  list(): Observable<OrderSummary[]> {
    return this.http.get<OrderSummary[]>(this.base);
  }

  get(id: number): Observable<OrderDetail> {
    return this.http.get<OrderDetail>(`${this.base}/${id}`);
  }

  create(request: CreateOrderRequest): Observable<{ orderId: number }> {
    return this.http.post<{ orderId: number }>(this.base, request);
  }
}
