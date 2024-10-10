import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import {AuthenticationService} from '../Services/authentication.service';
import { Injectable } from '@angular/core';

@Injectable()
export class errorInterceptor implements HttpInterceptor {
  constructor(private authenticationService: AuthenticationService) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(catchError(err => {
        if (err.status === 401) {
            // auto logout if 401 response returned from api
            this.authenticationService.logout();
            location.reload();
        }

        const error = err.error.message || err.statusText;
        throw error;
    }));
}

}