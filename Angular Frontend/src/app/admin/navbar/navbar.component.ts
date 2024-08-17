import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../users/auth/services/auth.service';

@Component({
  selector: 'app-admin-navbar',
  standalone: true,
  imports: [],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent {

  router = inject(Router)

  constructor(private service: AuthService) {}

  //Logs out of the application
  logout() {
    this.service.logoutUser();
  }

  //Navigate to the reports component.
  goToReports() {
    this.router.navigate(['reports']);
  }

  //Navigate to the dashboard component.
  goToDashboard() {
    this.router.navigate(['dashboard']);
  }

}
