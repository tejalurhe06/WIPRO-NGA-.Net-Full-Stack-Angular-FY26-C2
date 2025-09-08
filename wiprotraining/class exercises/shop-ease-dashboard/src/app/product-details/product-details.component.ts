import { Component ,Input,Output,EventEmitter} from '@angular/core';
import { Product } from '../app.config';
import { CommonEngine } from '@angular/ssr/node';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
@Component({
  selector: 'app-product-details',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './product-details.component.html',
  styleUrl: './product-details.component.css'
})
export class ProductDetailsComponent {

  @Input() product!: Product;
  @Output() feedback = new EventEmitter<{ comment: string; rating: number }>();

  comment: string = '';
  rating: number = 0;

  submitFeedback() {
    if (this.comment && this.rating > 0) {
      this.feedback.emit({ comment: this.comment, rating: this.rating });
      this.comment = '';
      this.rating = 0;
      alert('Feedback submitted!');
    } else {
      alert('Please provide comment and rating.');
    }
  }
}
