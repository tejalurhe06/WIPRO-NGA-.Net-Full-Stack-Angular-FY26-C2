import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-profile',
  imports: [],
  templateUrl: './profile.html',
  styleUrl: './profile.css'
})
export class Profile {

  userName : string|null=""
  constructor(private route:ActivatedRoute){
  }
    ngOnInit(){
      // this.userName=this.route.snapshot.paramMap.get('name')
      // console.log(this.userName)

      //if data passed in route file
      // this.route.queryParams.subscribe(params=>{
      //   this.userName=params['name']
      // })

      this.route.data.subscribe(data=>{
        this.userName = (data['name'])
      })
    }
  }
