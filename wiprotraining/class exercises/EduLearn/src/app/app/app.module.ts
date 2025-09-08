import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppComponent } from '../app.component';
import { CourseDetailComponent } from '../course-detail/course-detail.component';
import { CourseListComponent } from '../course-list/course-list.component';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';



@NgModule({
  declarations: [
    AppComponent,CourseDetailComponent,CourseListComponent
  ],
  imports: [
    CommonModule,FormsModule,BrowserModule
  ]
})
export class AppModule { }
