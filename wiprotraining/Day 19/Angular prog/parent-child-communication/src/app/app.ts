import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { User } from './user/user';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet,User],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('parent-child-communication');

  userName = "Kanishka"

  onUserChange(user:string){
    this.userName=user
  }
}
