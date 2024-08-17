import { Component, Inject, inject } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import {MatDialogRef, MAT_DIALOG_DATA, MatDialogModule,} from '@angular/material/dialog'
import {MatFormFieldModule} from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import {MatDatepickerModule} from '@angular/material/datepicker'
import { CommonModule } from '@angular/common';
import { CreateComponent } from '../create/create.component';
import { ClaimsService } from '../services/claims.service';
import { MatButtonModule } from '@angular/material/button';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

@Component({
  selector: 'app-edit-dialog',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatSelectModule,
    MatInputModule,
    MatDatepickerModule,
    CommonModule,
    MatButtonModule,
    MatProgressSpinnerModule
  ],
  templateUrl: './edit-dialog.component.html',
  styleUrl: './edit-dialog.component.css'
})
export class EditDialogComponent {
  editForm: FormGroup;
  email = localStorage.getItem('email');
  currencies: any[] = [];
  types: any[] = [];
  service = inject(ClaimsService);
  isLoading: Boolean = true;

  constructor(private fb: FormBuilder, private dialogRef: MatDialogRef<EditDialogComponent>,@Inject(MAT_DIALOG_DATA) public data: {id:any}){
    this.getCurrencies();
    this.getTypes();

    //Initialzing the edit form.
    this.editForm = this.fb.group({
      id: [data.id],
      email: fb.control(this.email,[Validators.required]),
      date: fb.control('',[Validators.required]),
      reimbursementTypeId: fb.control('',[Validators.required]),
      requestedValue: fb.control('',[Validators.required]),
      currencyId: fb.control('',[Validators.required]),
      image: fb.control('')
    })

    this.isLoading = true;
    //Fetching the claim based on the ID and patching it to the edit form.
    this.service.getClaim(data.id).subscribe((claim:any) => {
      this.editForm.patchValue(claim);
      this.isLoading = false;
    })
  }

  //Passing data after pop-up closes.
  editTask(): void {
    if (this.editForm.valid) {
      const formValue = this.editForm.value;
      if (formValue.date instanceof Date) {
        formValue.date = formValue.date.toISOString().split('T')[0];
      }
      const fileInput: HTMLInputElement | null = document.querySelector('#imageInput');
      console.log(fileInput?.files);
      if (fileInput?.files && fileInput.files[0]) {
        const reader = new FileReader();
        reader.onload = () => {
          formValue.image = reader.result;
          console.log("Worked")
          this.dialogRef.close(formValue);
        };
        reader.readAsDataURL(fileInput.files[0]);
      } else {
        console.log("failed")
        this.dialogRef.close(formValue);
      }
    }
  }

  //Cancels and closes dialog.
  onCancel(): void {
    this.dialogRef.close();
  }

  //Gets a list of currencies.
  getCurrencies() {
    this.service.getCurrencies().subscribe({
      next: (data) => {
        this.currencies = data;
      }
    })
  }

  //Gets a list of types.
  getTypes() {
    this.service.getTypes().subscribe({
      next: (data) => {
        this.types = data;
      }
    })
  }
}
