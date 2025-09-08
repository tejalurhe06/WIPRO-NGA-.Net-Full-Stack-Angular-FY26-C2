import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Product } from './product/product';
import { AuthModule } from './auth/auth-module';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet,Product,AuthModule],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('my-angular-app');
}
