import { NgFor } from '@angular/common';
import { Component } from '@angular/core';
import { EventEmitter,Output } from '@angular/core';

@Component({
  selector: 'app-course-list',
  imports: [NgFor],
  templateUrl: './course-list.component.html',
  styleUrl: './course-list.component.css'
})
export class CourseListComponent {

  courses = [
    { id: 1, title: 'Angular Basics', description: 'Learn the basics of Angular.' },
    { id: 2, title: 'Advanced Angular', description: 'Deep dive into Angular.' },
    { id: 3, title: 'Angular & RxJS', description: 'Reactive programming with Angular.' }
  ];

  @Output() courseSelected = new EventEmitter<any>();

  selectCourse(course: any) {
    this.courseSelected.emit(course);
  }

}
