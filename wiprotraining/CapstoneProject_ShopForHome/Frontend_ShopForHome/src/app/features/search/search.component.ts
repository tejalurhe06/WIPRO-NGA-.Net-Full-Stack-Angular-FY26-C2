import { Component, OnInit } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { SearchService } from "../../core/services/search.service";
import { CategoryService } from "../../core/services/category.service";
import { Category,Product } from "../../shared/models/models";
@Component({
  selector: "app-search",
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: "./search.component.html",
  styleUrls: ["./search.component.scss"]
})
export class SearchComponent implements OnInit {
  // ✅ Search filters
  searchTerm: string = "";
  categoryId: number | null = null;
  minPrice: number | null = null;
  maxPrice: number | null = null;
  minRating: number | null = null;

  // ✅ Data
  categories: Category[] = [];
  products: Product[] = [];

  constructor(
    private searchService: SearchService,     // ✅ use SearchService
    private categoryService: CategoryService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit() {
    // Load categories for filter dropdown
    this.categoryService.getAll().subscribe((res) => (this.categories = res));

    // Listen to query params (when coming from navbar/category click)
    this.route.queryParams.subscribe((params) => {
      this.searchTerm = params["term"] || "";
      this.categoryId = params["categoryId"] ? +params["categoryId"] : null;
      this.minPrice = params["minPrice"] ? +params["minPrice"] : null;
      this.maxPrice = params["maxPrice"] ? +params["maxPrice"] : null;
      this.minRating = params["minRating"] ? +params["minRating"] : null;

      this.onSearch(); // Run search when params change
    });
  }

  // ✅ Search function
  onSearch() {
    this.searchService
      .search({
        term: this.searchTerm,
        categoryId: this.categoryId || undefined,
        minPrice: this.minPrice || undefined,
        maxPrice: this.maxPrice || undefined,
        minRating: this.minRating || undefined
      })
      .subscribe((res) => (this.products = res));
  }
}
