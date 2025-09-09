import { Injectable } from '@angular/core';
import { delay, of } from 'rxjs';
import { Task } from '../models/task.model';

@Injectable({
  providedIn: 'root'
})
export class MockBackendService {
  private tasks: Task[] = [
    { id: 1, title: 'Learn Angular', description: 'Complete the Angular tutorial', status: 'pending' },
    { id: 2, title: 'Build a project', description: 'Create a task management app', status: 'completed' }
  ];
  private nextId = 3;

  getTasks() {
    return of([...this.tasks]).pipe(delay(500)); // Simulate network delay
  }

  addTask(task: Task) {
    const newTask = { ...task, id: this.nextId++ };
    this.tasks.push(newTask);
    return of(newTask).pipe(delay(300));
  }

  deleteTask(id: number) {
    this.tasks = this.tasks.filter(task => task.id !== id);
    return of(null).pipe(delay(300));
  }
}