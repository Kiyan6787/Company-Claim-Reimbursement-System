import { Component, Inject } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ClaimsService } from '../services/claims.service';
import { ActivatedRoute, Router } from '@angular/router';
import {MatFormFieldModule} from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import {MatDatepickerModule} from '@angular/material/datepicker'
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-edit',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatDatepickerModule,
    CommonModule
  ],
  templateUrl: './edit.component.html',
  styleUrl: './edit.component.css'
})
export class EditComponent {
  email: string | null = sessionStorage.getItem('email');
  id: string | null = '';
  currencies: any[] = [];
  types: any[] = [];

  constructor(private service: ClaimsService,private router: Router, private actRoute: ActivatedRoute, private fb: FormBuilder) {
    //Get the ID from the route.
    this.id = this.actRoute.snapshot.paramMap.get('id');

    this.getCurrencies();
    this.getTypes();

    //Fetches claim based on ID.
    if (this.id) {
      this.service.getClaim(this.id).subscribe((claim:any) => {
        this.editForm.patchValue(claim);
      })
    }
  }

  //Initializing edit form.
  editForm = new FormGroup({
      email: this.fb.control(this.email,[Validators.required]),
      date: this.fb.control('',[Validators.required]),
      reimbursementTypeId: this.fb.control('',[Validators.required]),
      requestedValue: this.fb.control('',[Validators.required]),
      currencyId: this.fb.control('',[Validators.required]),
      image: this.fb.control('', Validators.required)
  });

  editClaim() {
    const formVals = this.editForm.getRawValue();
    this.service.updateClaim(formVals, this.id).subscribe({
      next: () => {
        this.router.navigate(['claims']);
      },
      error: (err) => {
        console.error("Failed to update claim: ", err);
        alert("Failed to update claim");
      }
    })
  }

  getCurrencies() {
    this.service.getCurrencies().subscribe({
      next: (data) => {
        this.currencies = data;
      }
    })
  }

  getTypes() {
    this.service.getTypes().subscribe({
      next: (data) => {
        this.types = data;
      }
    })
  }
  
}
