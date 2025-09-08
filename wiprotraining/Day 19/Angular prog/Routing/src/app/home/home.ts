import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { Router } from 'express';

@Component({
  selector: 'app-home',
  imports: [RouterLink],
  templateUrl: './home.html',
  styleUrl: './home.css'
})
export class Home {

  constructor(private router:Router){

  }

  //if data passed in route file 
  // goToProfile(name:string){
  //   this.router.navigate(['profile',{queryParams:{ name:name}}])
  // }

  users=[
    {
      id:'1',
      name:'Tejal',
      age:24,
      email:'tejal@gmail.com'
    },
    {
      id:'2',
      name:'Kanuu',
      age:30,
      email:'Kanishka@gmail.com'
    },
    {
      id:'3',
      name:'Anshu',
      age:20,
      email:'Anshu@gmail.com'
    }
  ]
}
