// src/app/core/services/address.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { Address } from '../../shared/models/models';

@Injectable({ providedIn: 'root' })
export class AddressService {
  private base = '/addresses';

  constructor(private http: HttpClient) {}

  list() {
    return this.http.get<Address[]>(`${environment.apiBase}${this.base}`);
  }
}
