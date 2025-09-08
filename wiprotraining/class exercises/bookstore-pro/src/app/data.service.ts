import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable  ,of} from 'rxjs';

export interface Book {
  id: number;
  title: string;
  price: number;
  publicationDate: string;
  description: string;
  discount: number;
}

@Injectable({
  providedIn: 'root'
})
export class DataService {

  // private apiUrl = 'https://fakeapi.extendsclass.com/books';

  // constructor(private http: HttpClient) {}

  // getBooks(): Observable<Book[]> {
  //   return this.http.get<Book[]>(this.apiUrl);
  // }

  constructor(private http: HttpClient) {}

  // Replace API with mock for now
  getBooks(): Observable<Book[]> {
    return of([
      {
        id: 1,
        title: 'Angular Basics',
        price: 500,
        publicationDate: '2024-06-10',
        description: 'A beginner friendly guide to Angular.',
        discount: 10
      },
      {
        id: 2,
        title: 'Advanced Angular',
        price: 800,
        publicationDate: '2024-08-15',
        description: 'Deep dive into Angular core concepts.',
        discount: 15
      },
      {
        id: 3,
        title: 'RxJS in Depth',
        price: 600,
        publicationDate: '2024-09-20',
        description: 'Master reactive programming with RxJS.',
        discount: 5
      }
    ]);
  }
}
