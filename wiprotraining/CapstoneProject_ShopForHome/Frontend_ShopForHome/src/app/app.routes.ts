import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth.guard';

export const routes: Routes = [
  // Default → Products
  { path: '', redirectTo: 'products', pathMatch: 'full' },

  { path: 'login', loadComponent: () => import('./features/auth/login/login.component').then(m => m.LoginComponent) },
  { path: 'register', loadComponent: () => import('./features/auth/register/register.component').then(m => m.RegisterComponent) },
  { path: 'products', loadComponent: () => import('./features/catalog/products-list/products-list.component').then(m => m.ProductListComponent) },
  { path: 'product/:id', loadComponent: () => import('./features/catalog/product-detail/product-detail.component').then(m => m.ProductDetailComponent) },

  // Protected routes
  { path: 'cart', canActivate: [authGuard], loadComponent: () => import('./features/cart/cart.component').then(m => m.CartComponent) },
  { path: 'wishlist', canActivate: [authGuard], loadComponent: () => import('./features/wishlist/wishlist.component').then(m => m.WishlistComponent) },
  { path: 'categories', loadComponent: () => import('./features/catalog/categories/categories.component').then(m => m.CategoriesComponent) },
  { path: 'search', loadComponent: () => import('./features/search/search.component').then(m => m.SearchComponent) },
  { path: 'orders', canActivate: [authGuard], loadComponent: () => import('./features/order-list/order-list.component').then(m => m.OrderListComponent) },
 { path: 'orders/:id', canActivate: [authGuard], loadComponent: () => import('./features/order-detail/order-detail.component').then(m => m.OrderDetailComponent) },

  // Admin
  {
  path: 'admin',
  canActivate: [authGuard],
  children: [
    { path: 'dashboard', loadComponent: () => import('./features/admin/dashboard/dashboard.component').then(m => m.DashboardComponent) },
    { path: 'products', loadComponent: () => import('./features/admin/products/admin-products/admin-products.component').then(m => m.AdminProductsComponent) },
    { path: 'upload-csv', loadComponent: () => import('./features/admin/upload-csv/upload-csv.component').then(m => m.UploadCsvComponent) },
    { path: 'users', loadComponent: () => import('./features/admin/users/admin-users/admin-users.component').then(m => m.AdminUsersComponent) },
    { path: 'orders', loadComponent: () => import('./features/admin/orders/admin-orders/admin-orders.component').then(m => m.AdminOrdersComponent) },
    { path: 'coupons', loadComponent: () => import('./features/admin/coupons/admin-coupons/admin-coupons.component').then(m => m.AdminCouponsComponent) },
    { path: 'reports', loadComponent: () => import('./features/admin/reports/admin-reports/admin-reports.component').then(m => m.AdminReportsComponent) },
    { path: '', redirectTo: 'dashboard', pathMatch: 'full' } // default admin route
  ]
},
  { path: '**', redirectTo: 'products' }
];
