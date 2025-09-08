import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Post } from './post/post';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet,Post],
  template: 
  `<app-post></app-post>`,
  standalone:true,
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('fetch-api');
}
