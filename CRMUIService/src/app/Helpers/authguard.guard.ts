import { CanActivateFn } from '@angular/router';
import {inject} from '@angular/core';
import {Router} from '@angular/router';
import {AuthenticationService} from '../Services/authentication.service';

export const authguardGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);
  const token = inject(AuthenticationService).getToken;
  if(token && token.length > 0)
    return true;

  router.navigateByUrl('login');
  return false;
};
