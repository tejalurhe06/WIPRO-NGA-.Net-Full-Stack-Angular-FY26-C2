import { Component } from '@angular/core';
import { DataServive } from '../data-servive';
import { CommonModule } from '@angular/common';
import { OnInit } from '@angular/core';

@Component({
  selector: 'app-post',
  imports: [CommonModule],
  template : 
  `<ul>
  <li *ngFor = "let post of posts">{{post.title}}</li>
  </ul>`
  ,
//  styleUrl: './post.css'
})

export class Post implements OnInit {

  posts : any[] = [];
  constructor(private DataService : DataServive){

  }

  ngOnInit(): void {
    this.DataService.getPosts().subscribe((data)=>{
      this.posts = data;
    }
    );
  }
}
