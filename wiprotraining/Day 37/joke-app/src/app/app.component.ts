import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule],
  template: `
    <h1>Random Joke</h1>
    <button (click)="getJoke()">Get Joke</button>

    <div *ngIf="joke">
      <p><strong>{{ joke.setup }}</strong></p>
      <p>{{ joke.punchline }}</p>
    </div>
  `
})
export class AppComponent {
  joke: any;

  constructor(private http: HttpClient) {}

  getJoke() {
    this.http.get('https://official-joke-api.appspot.com/random_joke')
      .subscribe((data: any) => {
        this.joke = data;
      });
  }
}
