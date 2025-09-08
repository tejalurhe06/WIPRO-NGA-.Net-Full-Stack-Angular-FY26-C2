import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Coupon } from "../../shared/models/models";
import { environment } from "../../../environments/environment";
import { ApplyCouponResult } from "../../shared/models/models";

@Injectable({ providedIn: 'root' })
export class CouponsService {
  private base = '/coupons';
  
  constructor(private http: HttpClient) {}
  
  myCoupons() { 
    return this.http.get<Coupon[]>(`${environment.apiBase}${this.base}`); 
  }
  
  apply(code: string) { 
    return this.http.post<ApplyCouponResult>(`${environment.apiBase}${this.base}/apply`, { code }); 
  }
}