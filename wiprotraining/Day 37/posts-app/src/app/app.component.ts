import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';       
import { FormsModule } from '@angular/forms';         
import { HttpClientModule, HttpClient } from '@angular/common/http';  
import { PostService } from './services/post.service';  

@Component({
  selector: 'app-root',
  standalone: true,          
  imports: [CommonModule, FormsModule, HttpClientModule], 
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  posts: any[] = [];
  searchTerm: string = '';
  errorMessage: string = '';

  constructor(private postService: PostService, private http: HttpClient) {
    this.getPosts();
  }

  getPosts() {
    this.postService.getPosts().subscribe({
      next: (data) => this.posts = data,
      error: () => this.errorMessage = 'Failed to load posts'
    });
  }

  searchPosts() {
    if (!this.searchTerm) {
      this.getPosts(); // reload all posts if search is empty
      return;
    }

    this.posts = this.posts.filter(post =>
      post.title.toLowerCase().includes(this.searchTerm.toLowerCase())
    );

    if (this.posts.length === 0) {
      this.errorMessage = 'No posts found for your search';
    } else {
      this.errorMessage = '';
    }
  }
}
