import { CommonModule} from '@angular/common';
import { Component,OnDestroy,OnInit } from '@angular/core';
import { BookDetailComponent } from '../book-detail/book-detail.component';
import { Book,DataService } from '../data.service';
import { Subscription ,Observable} from 'rxjs';

@Component({
  selector: 'app-books',
  standalone: true,
  imports: [CommonModule,BookDetailComponent],
  templateUrl: './books.component.html',
  styleUrl: './books.component.css'
})
export class BooksComponent implements OnInit {

  booksManual: Book[] = [];
  booksAsync$!: Observable<Book[]>;

  constructor(private dataService: DataService) {}

  ngOnInit(): void {
    // Manual subscribe
    this.dataService.getBooks().subscribe(data => {
      this.booksManual = data;
    });

    // Async pipe
    this.booksAsync$ = this.dataService.getBooks();
  }
}
