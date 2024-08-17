import { Component } from '@angular/core';
import { ReactiveFormsModule,FormGroup, FormControl, Validators, FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';
import { MatSelectModule } from '@angular/material/select';
import { createPasswordValidator } from '../../../validators/custom-validator';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    CommonModule,
    FormsModule,
    MatSelectModule
  ],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  confirmPassword: string = '';
  passwordMatch: boolean = true;
  banks: any[] = [];

  constructor(private service: AuthService, private router: Router) {
    console.log(this.banks)
    this.getBanks();
  }

  //Initializing register form.
  registerForm = new FormGroup({
    fullName: new FormControl('',[Validators.required, Validators.pattern('^[a-zA-Z]+(?: [a-zA-Z]+)+$')]),
    email: new FormControl('',[Validators.required, Validators.email]),
    PANNumber: new FormControl('', [Validators.required, Validators.minLength(10), Validators.maxLength(10)]),
    bankId: new FormControl('', [Validators.required]),
    bankAccountNumber: new FormControl('', [Validators.required, Validators.pattern(/^\d+$/)]),
    password: new FormControl('',[Validators.required, Validators.minLength(8), createPasswordValidator()])
  });

  //Method for registering a user.
  register() {
    if (this.registerForm.get('password')?.value !== this.confirmPassword){
      this.passwordMatch = false;
      return;
    }

    const formVals = this.registerForm.getRawValue();

    this.service.register(formVals).subscribe({
      next: () => {
        alert("Registration Successful");
        this.router.navigate(['claims']);
      },
      error: (err) => {
        console.error("Registration error: ", err);
        alert("Registration Failed");
      }
    })

  }

  //Password validations. Checks if password entered matches confirm password.
  checkPassword(){
    this.passwordMatch = this.registerForm.get('password')?.value === this.confirmPassword;
  }

  //Gets a list of banks.
  getBanks() {
    this.service.getBanks().subscribe({
      next: (data) => {
        this.banks = data;
      },
      error: (err) => {
        console.error("Failed to get banks: ", err)
      }
    })
  }


}
