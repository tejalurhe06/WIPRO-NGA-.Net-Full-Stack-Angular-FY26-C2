import { Component, inject, output } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Task } from '../../models/task.model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-task-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './task-form.component.html',
  styleUrls: ['./task-form.component.css']
})
export class TaskFormComponent {
  private fb = inject(FormBuilder);
  taskAdded = output<Task>();

  taskForm = this.fb.group({
    title: ['', [Validators.required, Validators.minLength(3)]],
    description: ['', [Validators.required, Validators.minLength(5)]]
    // Removed dueDate for now to fix the errors
    // dueDate: [null] - This was causing the issues
  });

  onSubmit() {
    if (this.taskForm.valid) {
      const newTask: Task = {
        title: this.taskForm.value.title!,
        description: this.taskForm.value.description!,
        status: 'pending'
        // dueDate will be added later when we implement it properly
      };
      
      this.taskAdded.emit(newTask);
      this.taskForm.reset();
    }
  }
}