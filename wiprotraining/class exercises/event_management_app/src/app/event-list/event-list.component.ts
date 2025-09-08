import { Component } from '@angular/core';
import { CommonModule, CurrencyPipe, DatePipe } from '@angular/common';
import { HighlightDirective } from '../highlight.directive';
import { PriceFormatPipe } from '../price-format.pipe';
import { EventItem } from '../event.model';
@Component({
  selector: 'app-event-list',
  standalone: true,
  imports: [CommonModule, HighlightDirective, DatePipe, PriceFormatPipe],
  templateUrl: './event-list.component.html',
  styleUrl: './event-list.component.css'
})
export class EventListComponent {

  events: EventItem[] = [
    { name: 'Tech Innovators Conference', date: '2025-09-12', price: 3500, category: 'Conference' },
    { name: 'Creative Writing Workshop', date: '2025-10-05', price: 800, category: 'Workshop' },
    { name: 'Rock Music Concert', date: '2025-11-20', price: 2500, category: 'Concert' },
    { name: 'AI & Machine Learning Summit', date: '2025-12-02', price: 5000, category: 'Conference' }
  ];
}
