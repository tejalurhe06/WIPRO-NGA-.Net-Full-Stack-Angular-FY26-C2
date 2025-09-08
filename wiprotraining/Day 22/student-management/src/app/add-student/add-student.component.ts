import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { StudentService,IStudent } from '../student.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-add-student',
  standalone : true,
  imports: [FormsModule,CommonModule],
  templateUrl: './add-student.component.html',
  styleUrl: './add-student.component.css'
})
export class AddStudentComponent {

  student: IStudent = { id: 0, name: '', age: 0, course: '' };

  constructor(private studentService: StudentService, private router: Router) {}

  addStudent() {
    this.studentService.addStudent({ ...this.student, id: Date.now() });
    this.router.navigate(['/students']);
  }
}
