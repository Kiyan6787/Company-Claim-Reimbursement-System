import { Component } from '@angular/core';
import { AuthService } from '../users/auth/services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent {

  email = localStorage.getItem('email');
  
  constructor(private service: AuthService, private router: Router) {}

  logout() {
    this.service.logoutUser();
  }

  navigateToClaims() {
    this.router.navigate(['claims']);
  }
}
