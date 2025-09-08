import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Product } from '../../shared/models/models';

@Injectable({ providedIn: 'root' })
export class ProductService {
  private base = '/products';

  constructor(private http: HttpClient) {}

  getAll() {
    return this.http.get<Product[]>(`${environment.apiBase}${this.base}`);
  }

  getById(id: number) {
    return this.http.get<Product>(`${environment.apiBase}${this.base}/${id}`);
  }
}

