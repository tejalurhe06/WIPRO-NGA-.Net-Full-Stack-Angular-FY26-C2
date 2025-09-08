import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { CourseDetailComponent } from './course-detail/course-detail.component';
import { CourseListComponent } from './course-list/course-list.component';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet,CourseDetailComponent,CourseListComponent],
  template : `
    <div class="container">
      <h1 class="mt-3">EduLearn Course Management</h1>
      <app-course-list (courseSelected)="selectedCourse = $event"></app-course-list>
      <app-course-detail [course]="selectedCourse"></app-course-detail>
    </div>
  `,
  styleUrl: './app.component.css'
})
export class AppComponent {
  selectedCourse: any;
}
