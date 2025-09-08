import { Component, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet,FormsModule],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('Two-way-binding');

  name="Tejal"

  changeName(event : Event)
  {
    const val = (event.target as HTMLInputElement).value
    this.name = val
  }

  color = "red"
  //fontSize= "80 px";
  fontSize= "80"
   
}
