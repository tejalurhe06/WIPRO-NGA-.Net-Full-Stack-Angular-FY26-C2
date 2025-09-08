import { Component } from '@angular/core';

@Component({
  selector: 'app-stud-details',
  imports: [],
  templateUrl: './stud-details.html',
  styleUrl: './stud-details.css'
})
export class StudDetails {
public students = [
  {"id" : 1001, "name":"Tejal" , "marks":90},
  {"id" : 1002, "name":"kanishka" , "marks":80},
  {"id" : 1003, "name":"anshu" , "marks":70},
  {"id" : 1004, "name":"pranjal" , "marks":985},

]

constructor(){

}

ngOnInit(){


}

}
