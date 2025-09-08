import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  //templateUrl: './app.html',
  template:
  `<h1>{{title()}}</h1>
   <p>Welcome to Template Demo</p>
  `,
  //styleUrl: './app.css'
  styles:`
  h1{
  background-color:green
  }`
})
export class App {
  protected readonly title = signal('templateDemo');
}
