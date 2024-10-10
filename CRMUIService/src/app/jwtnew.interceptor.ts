import {inject} from '@angular/core';
import { HttpInterceptorFn } from '@angular/common/http';
import { User } from './Models/user.model';
import {AuthenticationService} from '../app/Services/authentication.service';

export const jwtnewInterceptor: HttpInterceptorFn = (req, next) => {
  const token = inject(AuthenticationService).getToken;
  console.log("interceptor called");
console.log(token);

if(token && token.length> 0)
{
  console.log('if part');
 var reqst = req.clone({
    headers: req.headers.set('Authorization', 'Bearer '+token),
  });

  
   console.log(reqst);
  return next(reqst);
}
else
return next (req);
};
