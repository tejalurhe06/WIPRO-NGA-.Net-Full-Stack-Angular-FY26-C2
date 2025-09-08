// =================== USER ===================
export interface User {
  userId?: number;
  firstName: string;
  lastName?: string;
  email: string;
  userType: number;              // maybe replace with enum later
  role: 'Admin' | 'User' | null; // backend alignment
  createdAt?: string;
  isActive?: boolean;
}

export interface Address {
  addressId: number;
  street: string;
  city: string;
  state: string;
  postalCode: string;
}

export interface LoginRequest {
  email: string;
  password: string;
}

export interface LoginResponse {
  token: string;
  user: User;
}

// =================== PRODUCT ===================
export interface Product {
  productId: number;
  name: string;
  description: string;
  price: number;
  stockQuantity: number;
  imageUrl: string;
  categoryId: number;
  categoryName?: string;
  averageRating?: number;
  isActive?: boolean;
  createdAt?: Date;
}

// =================== CATEGORY ===================
export interface Category {
  categoryId: number;
  categoryName: string;
}

// =================== CART ===================
export interface CartItem {
  cartItemId: number;
  cartId: string;
  productId: number;
  quantity: number;
  addedAt: string;
  product: Product;
}

// =================== COUPON ===================
export interface Coupon {
  couponId: number;
  code: string;
  description?: string;
  discountType: 'Percentage' | 'Fixed';
  discount: number;
  validFrom: string;
  validTo: string;
  minPurchase?: number;
  maxDiscount?: number;
}

export interface ApplyCouponResult {
  discount: number;
  finalTotal: number;
  couponCode: string;
}

// =================== ORDER ===================
export interface OrderSummary {
  orderId: number;
  orderDate: string;
  orderTotal: number;
  orderStatus: string;
}

export interface OrderDetail extends OrderSummary {
  shippingAddress: Address;
  orderItems: OrderItem[];
}

export interface OrderItem {
  orderItemId: number;
  productId: number;
  quantity: number;
  price: number;
  productName: string;
  imageUrl?: string;
}

export interface CreateOrderRequest {
  addressId: number;
}
