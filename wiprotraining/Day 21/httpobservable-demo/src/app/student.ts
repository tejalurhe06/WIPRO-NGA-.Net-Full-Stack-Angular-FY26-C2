import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})

export class Student {
  public students = [
  {"id" : 1001, "name":"Tejal" , "marks":90},
  {"id" : 1002, "name":"kanishka" , "marks":80},
  {"id" : 1003, "name":"anshu" , "marks":70},
  {"id" : 1004, "name":"pranjal" , "marks":985},

]

constructor(){

}

getStudents(){
  return this.students
}

}
