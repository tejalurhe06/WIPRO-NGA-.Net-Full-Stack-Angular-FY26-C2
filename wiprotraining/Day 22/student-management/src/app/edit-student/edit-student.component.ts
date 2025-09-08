import { Component, OnInit } from '@angular/core';
import { ActivatedRoute,Router } from '@angular/router';
import { StudentService,IStudent } from '../student.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-edit-student',
  imports: [CommonModule,FormsModule],
  templateUrl: './edit-student.component.html',
  styleUrl: './edit-student.component.css'
})
export class EditStudentComponent implements OnInit{

  student: IStudent = { id: 0, name: '', age: 0, course: '' };

  constructor(
    private route: ActivatedRoute,
    private studentService: StudentService,
    private router: Router
  ) {}

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    const foundStudent = this.studentService.getStudentById(id);
    if (foundStudent) this.student = { ...foundStudent };
  }

  updateStudent() {
    this.studentService.updateStudent(this.student);
    this.router.navigate(['/students']);
  }
}
