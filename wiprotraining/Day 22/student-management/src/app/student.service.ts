import { Injectable } from '@angular/core';

export interface IStudent {
  id: number;
  name: string;
  age: number;
  course: string;
}

@Injectable({
  providedIn: 'root'
})
export class StudentService {

  private students: IStudent[] = [
    { id: 1, name: 'Kanishka', age: 20, course: 'IT' },
    { id: 2, name: 'Tejal', age: 21, course: 'CS' }
  ];

  getStudents(): IStudent[] {
    return this.students;
  }

  addStudent(student: IStudent) {
    this.students.push(student);
  }

  updateStudent(updatedStudent: IStudent) {
    const index = this.students.findIndex(s => s.id === updatedStudent.id);
    if (index !== -1) this.students[index] = updatedStudent;
  }

  deleteStudent(id: number) {
    this.students = this.students.filter(s => s.id !== id);
  }

  getStudentById(id: number) {
    return this.students.find(s => s.id === id);
  }
}
