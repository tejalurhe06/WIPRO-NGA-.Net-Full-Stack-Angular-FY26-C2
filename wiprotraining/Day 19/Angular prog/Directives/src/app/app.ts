import { NgFor, NgIf, NgSwitch, NgSwitchCase, NgSwitchDefault } from '@angular/common';
import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet,NgIf,NgFor,NgSwitch,NgSwitchCase,NgSwitchDefault],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('Directives');

  //ngFor example

  students=["Tejal","Kanishka","Pranjal"]

  studentData = [
    {
      name : "Tejal",
      age: 27,
      email : "tejalurhe056@gmail.com"
    },
    {
      name : "kanishka",
      age: 50,
      email : "kannuu@gmail.com"
    },
    {
      name : "Pranjal",
      age: 60,
      email : "pc09@gmail.com"
    }
  ]

  //ngIf example
  show=false
  login=true

  block = 0
  updateBlock()
  {
    this.block++
  }

  //ngSwitch example
  color = "pink"

  changeColor(val:string)
  {
    this.color = val
  }
}
