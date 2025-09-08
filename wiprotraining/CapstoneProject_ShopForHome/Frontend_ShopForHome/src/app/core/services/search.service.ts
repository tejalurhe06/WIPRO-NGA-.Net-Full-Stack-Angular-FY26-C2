import { Injectable } from "@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http";
import { environment } from "../../../environments/environment";
import { Product } from "../../shared/models/models";
import { Observable } from "rxjs";

@Injectable({ providedIn: 'root' })
export class SearchService {
  private base = '/search';

  constructor(private http: HttpClient) {}

  search(params: { 
    term?: string; 
    categoryId?: number; 
    minPrice?: number;
    maxPrice?: number; 
    minRating?: number; 
  }): Observable<Product[]> {
    let httpParams = new HttpParams();

    if (params.term) httpParams = httpParams.set('term', params.term);
    if (params.categoryId) httpParams = httpParams.set('categoryId', params.categoryId.toString());
    if (params.minPrice) httpParams = httpParams.set('minPrice', params.minPrice.toString());
    if (params.maxPrice) httpParams = httpParams.set('maxPrice', params.maxPrice.toString());
    if (params.minRating) httpParams = httpParams.set('minRating', params.minRating.toString());

    return this.http.get<Product[]>(`${environment.apiBase}${this.base}`, {
      params: httpParams
    });
  }
}
