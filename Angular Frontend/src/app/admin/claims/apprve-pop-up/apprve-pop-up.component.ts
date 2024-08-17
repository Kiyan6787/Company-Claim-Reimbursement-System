import { Component, Inject, inject } from '@angular/core';
import {MatDialogRef, MAT_DIALOG_DATA, MatDialogModule,} from '@angular/material/dialog'
import {MatFormFieldModule} from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import {MatDatepickerModule} from '@angular/material/datepicker'
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { AdminService } from '../../services/admin.service';


@Component({
  selector: 'app-apprve-pop-up',
  standalone: true,
  imports: [
    MatDialogModule,
    MatFormFieldModule,
    MatSelectModule,
    MatInputModule,
    MatDatepickerModule,
    CommonModule,
    ReactiveFormsModule,
    MatButtonModule
  ],
  templateUrl: './apprve-pop-up.component.html',
  styleUrl: './apprve-pop-up.component.css'
})
export class ApprvePopUpComponent {
  approveForm: FormGroup;
  service = inject(AdminService);
  email: string | null = '';

  constructor(private fb: FormBuilder, private dialogRef: MatDialogRef<ApprvePopUpComponent>,@Inject(MAT_DIALOG_DATA) public data: {id:any, value:number}){
    
    // Fetch claim
    this.approveForm = this.fb.group({
      id: [data.id],
      approvedBy: ['', [Validators.required]],
      approvedValue: [data.value, [Validators.required]],
      internalNotes: [''],
      email: ['', [Validators.required]],
      date: ['', [Validators.required]],
      reimbursementTypeId: ['', [Validators.required]],
      requestedValue: ['', [Validators.required]],
      currencyId: ['', [Validators.required]],
      image: ['']
    });

    // Fetch email from localStorage and set it in the form
    this.email = localStorage.getItem('email');
    if (this.email) {
      this.approveForm.get('approvedBy')?.setValue(this.email);
    }

    // Fetch claim data
    this.service.getClaim(data.id).subscribe((claim: any) => {
      this.approveForm.patchValue({
        ...claim,
        approvedBy: this.approveForm.get('approvedBy')?.value,
        approvedValue: this.approveForm.get('approvedValue')?.value
      });
      console.log(claim);
    });

    console.log(this.email);

  }

  //Method for sending the data to the pending-requests component.
  approveClaim(): void {
    if (this.approveForm.valid) {
      const formValue = this.approveForm.value;
    // Only send the updated fields
    const updatedClaim = {
      id: formValue.id,
      approvedBy: formValue.approvedBy,
      approvedValue: formValue.approvedValue,
      internalNotes: formValue.internalNotes
    };
      console.log('Form data:', formValue);
      this.dialogRef.close(formValue);
    } else {
      console.error('Form is invalid');

      Object.keys(this.approveForm.controls).forEach(key => {
        const control = this.approveForm.get(key);
        console.log(`${key} - Valid: ${control?.valid}, Errors:`, control?.errors);
      });
    }
  }

  //Closes the pop-up form.
  onCancel(): void {
    this.dialogRef.close();
  }

  clearForm() {
    const id = this.approveForm.get('id')?.value;
    const email = this.approveForm.get('email')?.value;
    this.approveForm.reset({ id: id, email:email }); 
  }
}
