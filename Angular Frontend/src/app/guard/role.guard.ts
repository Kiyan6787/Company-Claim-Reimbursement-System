import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../users/auth/services/auth.service';

//Guard for preventing users without the required role from accessing certain routes.

export const roleGuard: CanActivateFn = (route, state) => {

  let router = inject(Router);
  let approver = JSON.parse(localStorage.getItem('isApprover') || 'false');

  if (!approver) {
    router.navigate(['access-denied']);
    return false;
  } else {
    return true;
  }
};
