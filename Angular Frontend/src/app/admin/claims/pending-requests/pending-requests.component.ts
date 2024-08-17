import { Component, TemplateRef, ViewChild } from '@angular/core';
import { AdminService } from '../../services/admin.service';
import { CommonModule } from '@angular/common';
import { MatDialog } from '@angular/material/dialog';
import { ApprvePopUpComponent } from '../apprve-pop-up/apprve-pop-up.component';
import { MatButtonModule } from '@angular/material/button';
import { RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner'

@Component({
  selector: 'app-pending-requests',
  standalone: true,
  imports: [
    CommonModule,
    MatButtonModule,
    FormsModule,
    MatProgressSpinnerModule
  ],
  templateUrl: './pending-requests.component.html',
  styleUrl: './pending-requests.component.css'
})
export class PendingRequestsComponent {

  claims : any[] = [];
  searchClaims: any[] = [];
  searchEmail: string = '';
  selectedReimbursementType: any = ''; 
  reimbursementTypes: any[] = []; 
  reimbursementTypeList: any[] = [];
  @ViewChild('imageDialog') imageDialog!: TemplateRef<any>;
  isLoading: boolean = true;

  constructor(private service: AdminService, private dialog: MatDialog){
    this.getTypes();
    this.getAllClaims();
  }

  //Fetch the claims.
  getAllClaims() {
    this.isLoading = true;
    this.service.getAllClaims().subscribe({
      next: (data) => {
        this.claims = data;
        this.searchClaims = data;
        this.isLoading = false;
        console.log(data);
      },
      error: (err) => {
        console.error("Failed to fetch claims: ", err);
        this.isLoading = false;
      }
    })
  }

  //Filter the claims based on a user email or reimbursement type.
  searchForClaims() {
    const searchTerm = this.searchEmail.toLowerCase();
    const selectedType = this.selectedReimbursementType ? +this.selectedReimbursementType : null; 

    console.log('Search Term:', searchTerm);
    console.log('Selected Type:', selectedType);

    this.searchClaims = this.claims.filter(claim => {
        const matchesEmail = claim.email.toLowerCase().includes(searchTerm);
        const matchesType = selectedType !== null ? claim.reimbursementTypeId === selectedType : true;

        return matchesEmail && matchesType;
    });
  } 

  //Fetches all the types stored in the database.
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

  openImage(image: string) {
    this.dialog.open(this.imageDialog, {
      data: { image }
    });
  }

  //Opens the approve claim pop-up.
  openTaskForm(claimId:any, reqVal:number): void {
    const dialogRef = this.dialog.open(ApprvePopUpComponent, {
      width: '400px',
      data: {id:claimId, value: reqVal} 
    });

    //Handle the data that is recieved after the form is closed.
    dialogRef.afterClosed().subscribe(result => {
      console.log("Data from form: ",result);
      this.service.approveClaim(result, result.id).subscribe({
        next: () => {
          alert("Claim Approved");
          this.service.notifyClaimUpdated();
          this.getAllClaims();
        },
        error: (err) => {
          alert("Claim failed");
          console.error("Failed to approve: ", err);
        }
      })
    });
  }

  claim: any = '';

  //Method that declines a claim.
  decline(id: any) {
    if (confirm("Are you sure you want to decline this claim?")) {
      this.service.getClaim(id).subscribe({
        next: (data) => {
          this.claim=data;
        }
      });
  
      this,this.service.declineClaim(id).subscribe({
        next: () => {
          alert("Claim declined");
          this.service.notifyClaimUpdated();
          this.getAllClaims();
        },
        error: (err) => {
          console.log("Failed to decline:", err);
          alert("Failed to decline");
        }
      })
    }
  }
}
