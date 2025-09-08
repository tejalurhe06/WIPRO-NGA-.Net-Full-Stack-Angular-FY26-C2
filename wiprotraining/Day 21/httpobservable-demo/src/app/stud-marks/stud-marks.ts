import { Component } from '@angular/core';

@Component({
  selector: 'app-stud-marks',
  imports: [],
  templateUrl: './stud-marks.html',
  styleUrl: './stud-marks.css'
})
export class StudMarks {

  public students = [
  {"id" : 1001, "name":"Tejal" , "marks":90},
  {"id" : 1002, "name":"kanishka" , "marks":80},
  {"id" : 1003, "name":"anshu" , "marks":70},
  {"id" : 1004, "name":"pranjal" , "marks":95},

]

constructor(){

}

ngOnInit(){


}
}
