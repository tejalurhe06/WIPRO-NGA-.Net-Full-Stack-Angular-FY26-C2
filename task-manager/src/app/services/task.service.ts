import { Injectable, inject, Inject, PLATFORM_ID } from '@angular/core';
import { isPlatformBrowser } from '@angular/common';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, catchError, throwError, of, delay } from 'rxjs';
import { Task } from '../models/task.model';

@Injectable({
  providedIn: 'root'
})
export class TaskService {
  private http = inject(HttpClient);
  private apiUrl = '/api/tasks';
  private isBrowser: boolean;
  
  private tasks: Task[] = [];
  private nextId = 1;

  constructor(@Inject(PLATFORM_ID) platformId: Object) {
    this.isBrowser = isPlatformBrowser(platformId);
    this.loadFromLocalStorage();
  }

  getTasks(): Observable<Task[]> {
    return of([...this.tasks]).pipe(
      delay(500),
      catchError(this.handleError)
    );
  }

  addTask(task: Task): Observable<Task> {
    const newTask: Task = { 
      ...task, 
      id: this.nextId++, 
      createdAt: new Date(),
      updatedAt: new Date()
    };
    this.tasks.push(newTask);
    this.saveToLocalStorage();
    return of(newTask).pipe(
      delay(300),
      catchError(this.handleError)
    );
  }

  updateTask(updatedTask: Task): Observable<Task> {
    const index = this.tasks.findIndex(task => task.id === updatedTask.id);
    if (index !== -1) {
      this.tasks[index] = { 
        ...updatedTask, 
        updatedAt: new Date() 
      };
      this.saveToLocalStorage();
      return of(this.tasks[index]).pipe(
        delay(300),
        catchError(this.handleError)
      );
    }
    return throwError(() => new Error('Task not found'));
  }

  deleteTask(id: number): Observable<void> {
    this.tasks = this.tasks.filter(task => task.id !== id);
    this.saveToLocalStorage();
    return of(undefined).pipe(
      delay(300),
      catchError(this.handleError)
    );
  }

  private loadFromLocalStorage() {
    if (!this.isBrowser) {
      // If we're not in a browser, initialize with demo tasks
      this.initializeDemoTasks();
      return;
    }

    try {
      const savedTasks = localStorage.getItem('tasks');
      if (savedTasks) {
        this.tasks = JSON.parse(savedTasks);
        // Find the maximum ID to set nextId correctly
        const maxId = this.tasks.reduce((max, task) => 
          task.id && task.id > max ? task.id : max, 0);
        this.nextId = maxId + 1;
      } else {
        this.initializeDemoTasks();
        this.saveToLocalStorage();
      }
    } catch (error) {
      console.error('Error loading from localStorage:', error);
      this.initializeDemoTasks();
    }
  }

  private saveToLocalStorage() {
    if (!this.isBrowser) return;
    
    try {
      localStorage.setItem('tasks', JSON.stringify(this.tasks));
    } catch (error) {
      console.error('Error saving to localStorage:', error);
    }
  }

  private initializeDemoTasks() {
    this.tasks = [
      { 
        id: 1, 
        title: 'Learn Angular', 
        description: 'Complete the Angular tutorial', 
        status: 'pending',
        createdAt: new Date(),
        updatedAt: new Date()
      },
      { 
        id: 2, 
        title: 'Build a project', 
        description: 'Create a task management app', 
        status: 'completed',
        createdAt: new Date(),
        updatedAt: new Date()
      }
    ];
    this.nextId = 3;
  }

  private handleError(error: HttpErrorResponse) {
    let errorMessage = 'An unknown error occurred!';
    if (error.error instanceof ErrorEvent) {
      errorMessage = `Error: ${error.error.message}`;
    } else {
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }
    console.error(errorMessage);
    return throwError(() => new Error(errorMessage));
  }
}