import { Injectable } from "@angular/core";
import { environment } from "../../../environments/environment";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";

export interface SalesReportResponse {
  startDate: string;
  endDate: string;
  totalRevenue: number;
  totalItemsSold: number;
  products: SalesReportProduct[];
}

export interface SalesReportProduct {
  productName: string;
  totalQuantitySold: number;
  totalRevenue: number;
}

export interface LowStockProduct {
  productId: number;
  name: string;
  stockQuantity: number;
  price: number;
}

@Injectable({ providedIn: 'root' })
export class ReportsService {
  private base = '/reports';
  
  constructor(private http: HttpClient) {}
  
  lowStock(): Observable<LowStockProduct[]> { 
    return this.http.get<LowStockProduct[]>(`${environment.apiBase}${this.base}/low-stock`); 
  }
  
  sales(startDate: string, endDate: string): Observable<SalesReportResponse> { 
    return this.http.get<SalesReportResponse>(`${environment.apiBase}${this.base}/sales`, { 
      params: { startDate, endDate } 
    }); 
  }
}