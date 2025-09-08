import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { StudDetails } from './stud-details/stud-details';
import { StudMarks } from './stud-marks/stud-marks';
import { BrowserModule } from '@angular/platform-browser';
import { provideHttpClient } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Pipe,PipeTransform } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet,StudDetails,StudMarks,BrowserModule,FormsModule],
  templateUrl: './app.html',
  styleUrl: './app.css'
})


export class App{
  protected readonly title = signal('httpobservable-demo');
  

}
