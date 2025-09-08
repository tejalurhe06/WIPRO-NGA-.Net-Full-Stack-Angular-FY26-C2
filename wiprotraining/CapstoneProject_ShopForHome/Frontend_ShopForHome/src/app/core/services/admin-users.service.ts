import { Injectable } from "@angular/core";
import { environment } from "../../../environments/environment";
import { HttpClient } from "@angular/common/http";

@Injectable({ providedIn: 'root' })
export class AdminUsersService {
  private base = '/admin/adminusers';

  constructor(private http: HttpClient) {}

  list() {
    return this.http.get<any[]>(`${environment.apiBase}${this.base}`);
  }

  get(id: number) {
    return this.http.get(`${environment.apiBase}${this.base}/${id}`);
  }

  update(id: number, u: any) {
    return this.http.put(`${environment.apiBase}${this.base}/${id}`, { ...u, userId: id });
  }

  delete(id: number) {
    return this.http.delete(`${environment.apiBase}${this.base}/${id}`);
  }
}
