
import { Injectable } from '@angular/core';

import {
  HttpInterceptor,
  HttpRequest,
  HttpHandler,
} from '@angular/common/http';


@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  intercept(req: HttpRequest<any>, next: HttpHandler) {
    const clonedReq = req.clone({
      setHeaders: { Authorization: 'Bearer dummy-token' }
    });
    return next.handle(clonedReq);
  }
}

