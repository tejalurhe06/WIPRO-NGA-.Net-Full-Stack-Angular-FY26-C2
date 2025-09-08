// import { HttpClient } from "@angular/common/http";
// import { environment } from "../../../environments/environment";
// import { Category } from "../../shared/models/models";
// import { Injectable } from "@angular/core";

// @Injectable({ providedIn: 'root' })
// export class CategoryService {
//   private base = '/categories';
  
//   constructor(private http: HttpClient) {}
  
//   getAll() { 
//     return this.http.get<Category[]>(`${environment.apiBase}${this.base}`); 
//   }
// }

import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { environment } from "../../../environments/environment";
import { Category } from "../../shared/models/models";

@Injectable({ providedIn: 'root' })
export class CategoryService {
  private base = '/categories';

  constructor(private http: HttpClient) {}

  // Get all categories
  getAll() {
    return this.http.get<Category[]>(`${environment.apiBase}${this.base}`);
  }

  // Get single category by id
  getById(id: number) {
    return this.http.get<Category>(`${environment.apiBase}${this.base}/${id}`);
  }

  // Create category (Admin only)
  create(category: Category) {
    return this.http.post<Category>(`${environment.apiBase}${this.base}`, category);
  }

  // Update category (Admin only)
  update(id: number, category: Category) {
    return this.http.put(`${environment.apiBase}${this.base}/${id}`, category);
  }

  // Delete category (Admin only)
  delete(id: number) {
    return this.http.delete(`${environment.apiBase}${this.base}/${id}`);
  }
}
