import { bootstrapApplication } from '@angular/platform-browser';
import { AppComponent } from './app/app.component';
import { provideHttpClient ,withInterceptorsFromDi} from '@angular/common/http';



bootstrapApplication(AppComponent,{
  providers: [
    provideHttpClient(withInterceptorsFromDi()), // Provides HttpClient globally
    
  ]
}).catch(err => console.error(err));
