import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TaskFormComponent } from './components/task-form/task-form.component';
import { TaskListComponent } from './components/task-list/task-list.component';
import { TaskService } from './services/task.service';
import { Task } from './models/task.model';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, TaskFormComponent, TaskListComponent],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  private taskService = inject(TaskService);
  tasks: Task[] = [];
  errorMessage: string | null = null;
  isLoading = true;

  ngOnInit() {
    this.loadTasks();
  }

  loadTasks() {
    this.isLoading = true;
    this.errorMessage = null;
    
    this.taskService.getTasks().subscribe({
      next: (tasks) => {
        this.tasks = tasks;
        this.isLoading = false;
      },
      error: (error) => {
        this.errorMessage = 'Failed to load tasks. Please try again later.';
        this.isLoading = false;
        console.error('Error loading tasks:', error);
      }
    });
  }

  addTask(newTask: Task) {
    this.errorMessage = null;
    
    this.taskService.addTask(newTask).subscribe({
      next: (task) => {
        this.tasks = [...this.tasks, task];
      },
      error: (error) => {
        this.errorMessage = 'Failed to add task. Please try again.';
        console.error('Error adding task:', error);
      }
    });
  }

  updateTask(updatedTask: Task) {
    this.errorMessage = null;
    
    this.taskService.updateTask(updatedTask).subscribe({
      next: (task) => {
        this.tasks = this.tasks.map(t => t.id === task.id ? task : t);
      },
      error: (error) => {
        this.errorMessage = 'Failed to update task. Please try again.';
        console.error('Error updating task:', error);
      }
    });
  }

  deleteTask(id: number) {
    this.errorMessage = null;
    
    this.taskService.deleteTask(id).subscribe({
      next: () => {
        this.tasks = this.tasks.filter(task => task.id !== id);
      },
      error: (error) => {
        this.errorMessage = 'Failed to delete task. Please try again.';
        console.error('Error deleting task:', error);
      }
    });
  }
}