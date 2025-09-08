import { CommonModule, CurrencyPipe, DatePipe, SlicePipe, UpperCasePipe } from '@angular/common';
import { Component,Input } from '@angular/core';
import { DiscountPipe } from '../discount.pipe';
import { Book } from '../data.service';

@Component({
  selector: 'app-book-detail',
  standalone: true,
  imports: [CommonModule,CurrencyPipe,DatePipe,UpperCasePipe,SlicePipe,DiscountPipe],
  templateUrl: './book-detail.component.html',
  styleUrl: './book-detail.component.css'
})
export class BookDetailComponent {

  @Input() book!: Book;
}
