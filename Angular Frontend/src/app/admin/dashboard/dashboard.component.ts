import { Component } from '@angular/core';
import {MatTabsModule} from '@angular/material/tabs';
import { PendingRequestsComponent } from "../claims/pending-requests/pending-requests.component";
import { NavbarComponent } from "../navbar/navbar.component";
import { ApprovedClaimsComponent } from "../claims/approved-claims/approved-claims.component";
import { DeclinedClaimsComponent } from "../claims/declined-claims/declined-claims.component";
import { AdminService } from '../services/admin.service';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [
    MatTabsModule,
    PendingRequestsComponent,
    NavbarComponent,
    ApprovedClaimsComponent,
    DeclinedClaimsComponent
],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent {

  constructor(private service: AdminService){}

  onTabChange(event: any) {
    this.service.notifyClaimUpdated();
  }
}
