import { Component, Inject, inject } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ClaimsService } from '../services/claims.service';
import { Router } from '@angular/router';
import {MatDialogRef, MAT_DIALOG_DATA, MatDialogModule,} from '@angular/material/dialog'
import {MatFormFieldModule} from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import {MatDatepickerModule} from '@angular/material/datepicker'
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-create',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatSelectModule,
    MatInputModule,
    MatDatepickerModule,
    CommonModule,
    MatButtonModule
  ],
  templateUrl: './create.component.html',
  styleUrl: './create.component.css'
})
export class CreateComponent {
  createForm: FormGroup;
  email = localStorage.getItem('email');
  currencies: any[] = [];
  types: any[] = [];
  service = inject(ClaimsService);

  constructor(private fb: FormBuilder, private dialogRef: MatDialogRef<CreateComponent>,@Inject(MAT_DIALOG_DATA) public data: any){
    this.getCurrencies();
    this.getTypes();

    //Initializing create form.
    this.createForm = this.fb.group({
      email: fb.control(this.email,[Validators.required]),
      date: fb.control('',[Validators.required]),
      reimbursementTypeId: fb.control('',[Validators.required]),
      requestedValue: fb.control('',[Validators.required]),
      currencyId: fb.control('',[Validators.required]),
      image: fb.control('')
    })
  }

  //Passing data after form close.
  createTask(): void {
    if (this.createForm.valid) {
      const formValue = this.createForm.value;
      formValue.date = formValue.date.toISOString().split('T')[0];

      const fileInput: HTMLInputElement | null = document.querySelector('#imageInput');
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

  //Closes the pop-up form.
  onCancel(): void {
    this.dialogRef.close();
  }

  //Get currency list.
  getCurrencies() {
    this.service.getCurrencies().subscribe({
      next: (data) => {
        this.currencies = data;
      }
    })
  }

  //Get reimbursement types.
  getTypes() {
    this.service.getTypes().subscribe({
      next: (data) => {
        this.types = data;
      }
    })
  }

  clearForm() {
    const emailValue = this.createForm.get('email')?.value;
    this.createForm.reset({ email: emailValue });
  }
}
