

import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';
import { CartService } from '../../../core/services/cart.service';
import { WishlistService } from '../../../core/services/wishlist.service';
import { CategoryService } from '../../../core/services/category.service';
import { Category } from '../../../shared/models/models';

@Component({
  selector: 'app-navbar',
  standalone: true,
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss'],
  imports: [CommonModule, RouterLink]
})
export class NavbarComponent implements OnInit {
  isLoggedIn = false;
  isAdmin = false;
  isUser = false;
  userName: string | null = null;
  cartCount = 0;
  wishlistCount = 0;

  categories: Category[] = []; // ✅ for dropdown

  constructor(
    private auth: AuthService,
    private router: Router,
    private cartService: CartService,
    private wishlistService: WishlistService,
    private categoryService: CategoryService // ✅ inject service
  ) {}

  ngOnInit() {
    this.updateAuthState();

    this.auth.currentUser$.subscribe(() => {
      this.updateAuthState();
      this.loadCartCount();
      this.loadWishlistCount();
    });

    this.loadCartCount();
    this.loadWishlistCount();

    // ✅ Load categories for navbar
    this.categoryService.getAll().subscribe(res => this.categories = res);
  }

  private updateAuthState() {
    this.isLoggedIn = this.auth.isAuthenticated();
    this.isAdmin = this.auth.isAdmin();
    this.isUser = this.auth.isUser();
    this.userName = this.auth.getFirstName();
  }

  private loadCartCount() {
    if (this.isLoggedIn) {
      this.cartService.list().subscribe((items: any[]) => {
        this.cartCount = items.reduce((total: number, item: any) => total + item.quantity, 0);
      });
    } else {
      this.cartCount = 0;
    }
  }

  private loadWishlistCount() {
    if (this.isLoggedIn) {
      this.wishlistService.list().subscribe((items: any[]) => {
        this.wishlistCount = items.length;
      });
    } else {
      this.wishlistCount = 0;
    }
  }

  logout() {
    this.cartService.clear().subscribe(() => {
      console.log('Cart cleared on logout');
    });
    this.wishlistCount = 0;
    this.auth.logout();
    this.router.navigate(['/login']);
  }
}
