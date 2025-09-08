import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root' // ✅ Important for standalone injection
})
export class WeatherService {
  private apiKey = '15e4d11494372ba4aed1e6c55ab6dd37';

  constructor(private http: HttpClient) {}

  getWeather(city: string): Observable<any> {
    const url = `https://api.openweathermap.org/data/2.5/weather?q=${city}&appid=${this.apiKey}&units=metric`;
    return this.http.get(url);
  }
}
