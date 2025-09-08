import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { WeatherService } from './services/weather.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, FormsModule],
  template: `
    <div style="text-align:center; margin-top:50px;">
      <h1>Weather Forecast</h1>

      <input [(ngModel)]="city" placeholder="Enter city name" />
      <button (click)="getWeather()">Get Weather</button>

      <div *ngIf="weatherData" style="margin-top:20px;">
        <h2>{{ weatherData.name }}, {{ weatherData.sys.country }}</h2>
        <p>Temperature: {{ weatherData.main.temp }}°C</p>
        <p>Weather: {{ weatherData.weather[0].description }}</p>
      </div>

      <div *ngIf="errorMessage" style="color:red; margin-top:20px;">
        {{ errorMessage }}
      </div>
    </div>
  `
})
export class AppComponent {
  city = '';
  weatherData: any = null;
  errorMessage = '';

  constructor(private weatherService: WeatherService) {}

  getWeather() {
    if (!this.city.trim()) {
      this.errorMessage = 'Please enter a city name!';
      this.weatherData = null;
      return;
    }

    this.weatherService.getWeather(this.city).subscribe({
      next: (data) => {
        this.weatherData = data;
        this.errorMessage = '';
      },
      error: () => {
        this.weatherData = null;
        this.errorMessage = 'City not found!';
      }
    });
  }
}
