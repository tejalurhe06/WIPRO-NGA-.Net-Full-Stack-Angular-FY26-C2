import { Component, computed, effect, Signal, signal, WritableSignal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { profile } from 'console';
import { Profile } from './profile/profile';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet,Profile],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('Angular-19-2');

  display =true;
  x=10
  togglediv = true

  hide(){
    this.display = false
  }

  show(){
    this.display = true
  }

  toggle(){
    this.display=!this.display
  }

  toggleTwo(){
    this.togglediv=!this.togglediv
  }

  color = 1;
  handleColor(val:number){
    this.color = val
  }

  handleInput(event:Event){
    this.color = parseInt((event.target as HTMLInputElement).value)
  }

  colour="green"

  handleColour(val:string){
    this.colour = val;
  }

  changeColour(event : Event){
    this.colour = (event.target as HTMLInputElement).value
  }

  users = ["Tejal","kanishka","Anshu", "Pranjal","Prachi","Sayli","Sakshi"]

  students = [
    {name:'Tejal',age:24,email:'tejalurhe056@gmail.com' },
    {name:'Kanishka',age:21,email:'kannu@gmail.com' },
    {name:'Anshu',age:25,email:'anshu21@gmail.com' },
  ]

  getName(name:string){
    console.log(name)
  }

  count1 = signal(10);
  x1=20

  constructor(){
    effect(()=>{
      console.log(this.x1)
      console.log(this.count1)
    })
  }

  // updateValue(){
  //   //this.x1=30
  //   this.count1.set(this.count1()+1)
  //   this.x1=this.x1 + 1
  // }

  updateValue(val:string){
    if(val=='inc')
    {
      this.count1.set(this.count1()+1)
    }
    else{
      this.count1.set(this.count1()-1)
    }
  }

  // data : WritableSignal<number | string> = signal<number | string>(10)
  data : WritableSignal<number | string> = signal(200)

  count2 : Signal<number> = computed(()=>10)

  updateSignal(){
    this.data.set("Hello")
    //this.count2.set(20) --error

    //this.data.update((vall)=>vall+1) ----for single datatype
  }

  // x3 = 10;
  // y = 20;
  // z = this.x3 + this.y

  // showValuee(){
  //   console.log(this.z) //30
  //   this.x3 = 100
  //   //this.z = this.x3+this.y
  //   console.log(this.z)  //30 
  // }

  x3 = signal(10);
  y = signal(20);
  z = computed(()=>this.x3()+ this.y())

  showValuee(){
    console.log(this.z) 
    //this.x3.set(100)
    console.log(this.z) 

  }

  updateX(){
    this.x3.set(200)
  }

  userName = signal('Tejal')

  constructor1(){
    effect(()=>{
      console.log(this.userName)
    })
  }
}
