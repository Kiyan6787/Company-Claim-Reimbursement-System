import { Component } from '@angular/core';
import { ClaimsService } from '../services/claims.service';
import { catchError, of } from 'rxjs';
import { CommonModule } from '@angular/common';
import { Router, RouterLink } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { CreateComponent } from '../create/create.component';
import { NavbarComponent } from "../../navbar/navbar.component";
import { EditComponent } from '../edit/edit.component';
import { MatButtonModule } from '@angular/material/button';
import { EditDialogComponent } from '../edit-dialog/edit-dialog.component';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import {MatPaginatorModule, PageEvent} from '@angular/material/paginator';

@Component({
  selector: 'app-claims-list',
  standalone: true,
  imports: [
    CommonModule,
    NavbarComponent,
    RouterLink,
    MatButtonModule,
    MatProgressSpinnerModule,
    MatPaginatorModule
],
  templateUrl: './claims-list.component.html',
  styleUrl: './claims-list.component.css'
})
export class ClaimsListComponent {
  claimsArray : any[] = [];
  requestPhase : string = '';
  isLoading: boolean = true;
  pageSize = 5;
  currentPage = 0;
  totalClaims = 0;
  paginateArray: any[] = [];

  constructor(private service: ClaimsService, private router: Router, private dialog: MatDialog){
    this.getClaims();
  }

  //Fetching all claims
  getClaims() {
    this.service.getClaims().pipe(
      catchError(error => {
        console.error('Error fetching claims: ', error);
        this.isLoading = false;
        return of([]);
      })
    ).subscribe(data => {
      this.isLoading = true;
      this.claimsArray = data.map(claim => ({
        ...claim,
        status: claim.isProcessed ? 'Processed' : 'To be processed',
        receipt: claim.image ? 'Yes' : 'No',
        approved: claim.isProcessed,
      }));
      this.paginateArray = this.claimsArray;
      this.totalClaims = this.claimsArray.length;
      this.paginateTasks();
      this.isLoading = false;
      console.log(data)
    })
  }

  paginateTasks(event?: PageEvent): void {
    const startIndex = this.pageSize * this.currentPage;
    const endIndex = startIndex + this.pageSize;
    this.paginateArray = this.claimsArray.slice(startIndex, endIndex);
  }

  handlePageEvent(event: PageEvent): void {
    this.pageSize = event.pageSize;
    this.currentPage = event.pageIndex;
    this.paginateTasks();
  }

  navigateToClaim() {
    this.router.navigate(['createClaim']);
  }

  //Opens the create claim pop-up.
  openTaskForm(): void {
    const dialogRef = this.dialog.open(CreateComponent, {
      width: '400px',
      data: {} 
    });

    //Handles data recieved from the form.
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.service.createClaims(result).subscribe({
          next: () => {
            alert("Claim added");
            console.log("Claim: ", result);
            this.getClaims();
          },
          error: (err) => {
            console.error("Error adding claims: ", err);
            alert("Failed to add claim");
          }
        })
      }
    });
  }

  //Opens edit pop-up
  openEditForm(claimId: any,): void {
    const dialogRef = this.dialog.open(EditDialogComponent, {
      width: '400px',
      data: {id:claimId} 
    });

    //Handles form data after closing pop-up.
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.service.updateClaim(result,result.id).subscribe({
          next: () => {
            alert("Claim Updated")
            this.getClaims();
          },
          error: (err) => {
            alert("Failed to update");
            console.error("Failed to update: ",err);
          }
        })
      }
    });
  }

  //Makes a call to the delete api.
  deleteClaim(id: any) {
    if (confirm("Are you sure you want to delete the selected claim?")) {
      this.service.deleteClaim(id).subscribe({
        next: () => {
          alert('Claim deleted successfully');
          this.getClaims();
        },
        error: (err) => {
          console.error('Failed to delete claim; ', err);
          alert("Failed to delete claim");
        }
      })
    }
  }
  
}
