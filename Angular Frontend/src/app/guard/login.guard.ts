import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../users/auth/services/auth.service';

//Guard for preventing unauthorised users from accessing certain routes.

export const loginGuard: CanActivateFn = (route, state) => {

  const service = inject(AuthService);
  const router = inject(Router);

  if (service.isLoggedIn()) {
    return true;
  } else {
    router.navigate(['']);
    return false;
  }
};
