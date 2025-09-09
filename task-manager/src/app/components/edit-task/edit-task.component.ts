import { Component, inject, input, output, OnInit } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Task } from '../../models/task.model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-edit-task',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './edit-task.component.html',
  styleUrls: ['./edit-task.component.css']
})
export class EditTaskComponent implements OnInit {
  private fb = inject(FormBuilder);
  task = input.required<Task>();
  taskUpdated = output<Task>();
  cancelEdit = output<void>();

  taskForm = this.fb.group({
    title: ['', [Validators.required, Validators.minLength(3)]],
    description: ['', [Validators.required, Validators.minLength(5)]],
    status: ['pending', Validators.required]
    // Removed dueDate for now
  });

  ngOnInit() {
    // Initialize form with task data
    this.taskForm.patchValue({
      title: this.task().title,
      description: this.task().description,
      status: this.task().status
    });
  }

  onSubmit() {
    if (this.taskForm.valid) {
      const updatedTask: Task = {
        ...this.task(),
        title: this.taskForm.value.title!,
        description: this.taskForm.value.description!,
        status: this.taskForm.value.status as 'pending' | 'completed'
      };
      this.taskUpdated.emit(updatedTask);
    }
  }

  onCancel() {
    this.cancelEdit.emit();
  }
}