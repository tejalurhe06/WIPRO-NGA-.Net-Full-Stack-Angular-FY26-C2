import { Injectable } from "@angular/core";
import { environment } from "../../../environments/environment";
import { HttpClient } from "@angular/common/http";

@Injectable({ providedIn: "root" })
export class AdminOrdersService {
  private base = "/admin/adminorders";

  constructor(private http: HttpClient) {}

  list() {
    return this.http.get<any[]>(`${environment.apiBase}${this.base}`);
  }

  updateStatus(id: number, status: string) {
    // ✅ backend expects a raw string body
    return this.http.put(
      `${environment.apiBase}${this.base}/${id}/status`,
      JSON.stringify(status),
      { headers: { "Content-Type": "application/json" } }
    );
  }

  stats() {
    return this.http.get(`${environment.apiBase}${this.base}/stats`);
  }
}
