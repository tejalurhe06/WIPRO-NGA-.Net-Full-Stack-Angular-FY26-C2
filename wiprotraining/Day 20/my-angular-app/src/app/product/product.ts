import { Component } from '@angular/core';

@Component({
  selector: 'app-product',
  imports: [],
  templateUrl: './product.html',
  styleUrl: './product.css'
})
export class Product {
   productName : string = 'Laptop';
   productPrice:number = 999.99
  
   constructor(){
    this.productName ='new sample product'
    this.productPrice=1000
   }

   message : string = 'Hello from Product'
   
   //method definition
   greetUser(name:string):string{
    return `Hello, ${name}! ${this.message}`
   }

   //Another method
   displayMessage():void{
    //console.log(this.message)
    alert(this.message)
   }
}
