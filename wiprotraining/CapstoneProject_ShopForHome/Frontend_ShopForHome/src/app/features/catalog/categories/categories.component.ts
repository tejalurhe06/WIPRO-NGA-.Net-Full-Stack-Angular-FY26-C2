// import { Component } from "@angular/core";
// import { CommonModule } from "@angular/common";
// import { OnInit } from "@angular/core";
// import { Category } from "../../../shared/models/models";
// import { RouterLink } from "@angular/router";
// import { CategoryService } from "../../../core/services/category.service";

// @Component({ 
//   standalone: true, 
//   imports: [CommonModule, RouterLink], 
//   templateUrl: './categories.component.html',
//   styleUrl: './categories.component.scss'
// })
// export class CategoriesComponent implements OnInit {
//   categories: Category[] = [];
  
//   constructor(private cats: CategoryService) {}
  
//   ngOnInit() { 
//     this.cats.getAll().subscribe(res => this.categories = res); 
//   }
// }

import { Component, OnInit } from "@angular/core";
import { CommonModule } from "@angular/common";
import { RouterLink } from "@angular/router";
import { Category } from "../../../shared/models/models";
import { CategoryService } from "../../../core/services/category.service";

@Component({
  selector: 'app-categories',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './categories.component.html',
  styleUrl: './categories.component.scss'
})
export class CategoriesComponent implements OnInit {
  categories: Category[] = [];
  loading = false;
  error: string | null = null;

  constructor(private cats: CategoryService) {}

  ngOnInit() {
    this.fetchCategories();
  }

  fetchCategories() {
    this.loading = true;
    this.error = null;

    this.cats.getAll().subscribe({
      next: (res) => {
        this.categories = res;
        this.loading = false;
      },
      error: (err) => {
        this.error = 'Failed to load categories';
        this.loading = false;
        console.error(err);
      }
    });
  }
}
