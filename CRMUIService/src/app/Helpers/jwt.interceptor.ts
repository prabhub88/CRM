import { HttpInterceptorFn } from '@angular/common/http';
import { HttpRequest, HttpHandler, HttpEvent,HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs';
import {AuthenticationService} from '../Services/authentication.service';
import { Injectable } from '@angular/core';

@Injectable()
export class jwtInterceptor implements HttpInterceptor {

  constructor(private authenticationService: AuthenticationService) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    console.log("interceptor called");
     
      if (1 > 0) {
          request = request.clone({
              setHeaders: {
                  Authorization: `Bearer ${""}`
              }
          });
      }

      return next.handle(request);
  }
};
