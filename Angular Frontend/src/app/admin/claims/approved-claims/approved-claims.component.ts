import { Component } from '@angular/core';
import { AdminService } from '../../services/admin.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-approved-claims',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule
  ],
  templateUrl: './approved-claims.component.html',
  styleUrl: './approved-claims.component.css'
})
export class ApprovedClaimsComponent {
  approvedClaims: any[] = [];
  searchClaims: any[] = [];
  searchEmail: string = '';
  selectedReimbursementType: any = ''; 
  reimbursementTypes: any[] = []; 
  reimbursementTypeList: any[] = [];
  private subscription!: Subscription;

  constructor(private service: AdminService){
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

  //Fetches all claims from the api.
  getClaims() {
    this.service.getApprovedClaims().subscribe({
      next: (data) => {
        this.approvedClaims = data;
        this.searchClaims = data;
      },
      error: (err) => {
        console.error("Failed to get claims: ", err)
      }
    });
  }

  //Filters for claims based on a user email or reimbursement type.
  searchForClaims() {
    const searchTerm = this.searchEmail.toLowerCase();
    const selectedType = this.selectedReimbursementType ? +this.selectedReimbursementType : null; 

    console.log('Search Term:', searchTerm);
    console.log('Selected Type:', selectedType);

    this.searchClaims = this.approvedClaims.filter(claim => {
        const matchesEmail = claim.email.toLowerCase().includes(searchTerm);
        const matchesType = selectedType !== null ? claim.reimbursementTypeId === selectedType : true;

        console.log('Claim Email:', claim.email.toLowerCase());
        console.log('Claim Type ID:', claim.reimbursementType?.id);
        console.log('Matches Email:', matchesEmail);
        console.log('Matches Type:', matchesType);

        return matchesEmail && matchesType;
    });
  } 

  //Fetches reimbursement types from the api.
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
