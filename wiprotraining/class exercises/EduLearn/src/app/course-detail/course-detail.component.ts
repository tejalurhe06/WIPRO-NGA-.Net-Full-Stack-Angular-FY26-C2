import { CommonModule, NgFor, NgIf } from '@angular/common';
import { Component,Input } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-course-detail',
  imports: [FormsModule,NgIf],
  templateUrl: './course-detail.component.html',
  styleUrl: './course-detail.component.css'
})
export class CourseDetailComponent {

    @Input() course: any;

}
