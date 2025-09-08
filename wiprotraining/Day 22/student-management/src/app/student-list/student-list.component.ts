import { Component, OnInit } from '@angular/core';
import { IStudent,StudentService } from '../student.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-student-list',
  imports: [CommonModule],
  templateUrl: './student-list.component.html',
  styleUrl: './student-list.component.css'
})
export class StudentListComponent {

  students: IStudent[] = [];

  constructor(private studentService: StudentService, private router: Router) {
    this.students = this.studentService.getStudents();
  }

  deleteStudent(id: number) {
    this.studentService.deleteStudent(id);
    this.students = this.studentService.getStudents();
  }

  editStudent(id: number) {
    this.router.navigate(['/edit-student', id]);
  }
}
