import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminService } from '../../services/admin.service';
import { FormsModule } from '@angular/forms';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-declined-claims',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule
  ],
  templateUrl: './declined-claims.component.html',
  styleUrl: './declined-claims.component.css'
})
export class DeclinedClaimsComponent {
  declinedClaims: any[] = [];
  searchClaims: any[] = [];
  searchEmail: string = '';
  selectedReimbursementType: any = ''; 
  reimbursementTypes: any[] = []; 
  reimbursementTypeList: any[] = [];
  private subscription!: Subscription;

  constructor(private service: AdminService) {
    this.getTypes();
  }

  ngOnInit() {
    this.getClaims();
    this.subscription = this.service.claimUpdated$.subscribe(() => {
      this.getClaims();
    })
  }

  ngOnDestroy() {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }

  //Fetches all claims from the database.
  getClaims() {
    this.service.getDeclinedClaims().subscribe({
      next: (data) => {
        this.declinedClaims = data;
        this.searchClaims = data;
      },
      error: (err) => {
        console.error("Failed to get claims: ", err);
      }
    })
  }

  //Filters the claims based on a user email or reimbursement type.
  searchForClaims() {
    const searchTerm = this.searchEmail.toLowerCase();
    const selectedType = this.selectedReimbursementType ? +this.selectedReimbursementType : null; 

    console.log('Search Term:', searchTerm);
    console.log('Selected Type:', selectedType);

    this.searchClaims = this.declinedClaims.filter(claim => {
        const matchesEmail = claim.email.toLowerCase().includes(searchTerm);
        const matchesType = selectedType !== null ? claim.reimbursementTypeId === selectedType : true;

        return matchesEmail && matchesType;
    });
  } 

  //Fetches reimbursement type.
  getTypes() {
    this.service.getTypes().subscribe({
      next: (data) => {
        this.reimbursementTypeList = data;
      },
      error: (err) => {
        console.warn("Failed to get types: ", err);
      }
    })
  }
}
