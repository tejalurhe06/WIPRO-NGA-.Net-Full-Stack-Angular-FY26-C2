import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Login } from './login/login';
import { Signup } from './signup/signup';
import { ProfileComponent } from './profile/profile';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet,Login,Signup,ProfileComponent],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('Angular-19');

  name = "Tejal";
  x=10;
  y=20;

  user1="peter";
  user2="tejal"

  handleClickEvent()
  {
    //alert("Function Called");
    console.log("function called");
    this.otherFunction();          //instance os current class
  }

  otherFunction()
  {
    console.log("Other function called");
  }

  name1 : string= 'Kanishka'  ;      //--property or name = 'kanishka' is same it understands the type
  data :string | number = 'hello';    //pipe
  //other : boolean | string | number = true
  other : any = true   //----------option for above but to use all time
  //let x = 30;        ----will give error here but it will work inside the function
  updateName(){
    //this.name = 20; ---------error
    this.name = 'petter'
    this.data = 123
    this.other = 'tejal'

    this.other = 30
    this.other = {}
  }

  updateVar(){
      let y=40
      let x : number= 30;
      console.log(x)
      this.sum(40,50)
    }

  sum(a:number,b:number){
    console.log(a+b)
  }

  count:number=0
  handleDecrement(){
    this.count =this.count-1
  }
  handleReset(){
    this.count =0
  }
  handleIncrement(){
    this.count =this.count+1
  }

  handleCounter(val : string){
    if(val == 'minus'){
      this.count =this.count-1
    }
    else if(val == 'plus')
    {
      this.count =this.count+1
    }
    else{
      this.count =0
    }

  }

  // handleEvent(event:MouseEvent){
  //   console.log("Function Called",event.type)
  //   console.log("Function Called",event.target)
  //   console.log("Function Called",(event.target as Element).className)
  //   console.log("Function Called",(event.target as Element).classList)
  // }

    handleEvent(event:Event)
    {
    console.log("Function Called",event.type)
    console.log("value",(event.target as HTMLInputElement).value)
    }
    
    name3=""
    displayName = ""
    email=""
    getName(event:Event)
    {
      this.name3 = (event.target as HTMLInputElement).value
      //console.log(name)
    }
    showName()
    {
      this.displayName = this.name3
    }

    setName()
    {
      this.name3 = "Sam"
    }

    getEmail(val:string)
    {
      console.log(val)
      this.email= val
    }

    setEmail()
    {
      this.email = "default@test.com"
    }
}
