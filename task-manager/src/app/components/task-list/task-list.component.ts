import { Component, inject, input, output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Task } from '../../models/task.model';
import { EditTaskComponent } from '../edit-task/edit-task.component';

@Component({
  selector: 'app-task-list',
  standalone: true,
  imports: [CommonModule, EditTaskComponent],
  templateUrl: './task-list.component.html',
  styleUrls: ['./task-list.component.css']
})
export class TaskListComponent {
  tasks = input.required<Task[]>();
  deleteTask = output<number>();
  updateTask = output<Task>();
  
  editingTaskId: number | null = null;

  onDeleteTask(id: number) {
    this.deleteTask.emit(id);
  }

  onEditTask(task: Task) {
    this.editingTaskId = task.id!;
  }

  onUpdateTask(updatedTask: Task) {
    this.updateTask.emit(updatedTask);
    this.editingTaskId = null;
  }

  onCancelEdit() {
    this.editingTaskId = null;
  }

  formatDate(date: Date | undefined): string {
    if (!date) return 'No date';
    return new Date(date).toLocaleDateString();
  }
}