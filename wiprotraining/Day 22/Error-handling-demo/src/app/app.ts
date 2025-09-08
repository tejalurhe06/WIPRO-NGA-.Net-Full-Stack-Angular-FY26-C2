import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet,CommonModule],
  template: 
  `
     <button (click)="doSomething()"> Do Something</button>
     <div *ngIf = "errorMessage" class="error-message">
     {{errorMessage}}
     </div>
  `,
  styles: ['.error-message{color:red}']
})


export class App {
  protected readonly title = signal('Error-handling-demo');

  errorMessage:string|null=null

  doSomething()
  {
    try{
      //Simulating error
      const data = JSON.parse('invalid json')
      console.log(data)
    }
    catch(error:any)
    {
      //handle error
      this.errorMessage = `An error occurred : ${error.message}`
      console.error('Synchronous error is caught :',error)
    }
  }
}
