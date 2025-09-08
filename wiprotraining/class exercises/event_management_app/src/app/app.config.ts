import { ApplicationConfig, importProvidersFrom, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideClientHydration, withEventReplay } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';;
import { HighlightDirective } from './highlight.directive';
import { PriceFormatPipe } from './price-format.pipe';


export const appConfig: ApplicationConfig = {
  providers: [provideZoneChangeDetection({ eventCoalescing: true }), provideRouter(routes), provideClientHydration(withEventReplay()),importProvidersFrom(CommonModule,HighlightDirective,PriceFormatPipe)]
};
