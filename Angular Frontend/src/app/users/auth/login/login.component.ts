import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';
import { AuthResponseDto } from '../../../_interfaces/AuthResponseDto';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  errMessage: string = "";

  constructor(private service: AuthService, private router: Router) {
    //Clearing local storage on application load.
    localStorage.clear();
    sessionStorage.clear();
  }

  //Initialize login form.
  loginForm = new FormGroup({
    Email: new FormControl('', [Validators.required, Validators.email]),
    Password: new FormControl('', [Validators.required])
  });

  //Method for logging in.
  login() {
    const formVals = this.loginForm.getRawValue();

    this.service.login(formVals).subscribe({
      next: () => {
        this.router.navigate(['claims'])
      },
      error: (err) => {
        console.log("Login Failed: ", err);
        alert("Login Failed");
      }
    })
  }

  //Method for login, this is the one in use.
  loginUser() {
    const formVals = this.loginForm.getRawValue();

    this.service.loginUser(formVals).subscribe({
      next: (res:AuthResponseDto) => {
        localStorage.setItem("token", res.token);
        localStorage.setItem('email', res.email);
        localStorage.setItem('isApprover', JSON.stringify(res.isApprover));
        if (res.isApprover) {
          this.router.navigate(['dashboard']);
        } 
        else {
          this.router.navigate(['claims']);
        }
      },
      error: (err) => {
        this.errMessage = "Invalid Email or Password"
        console.error("Error:", err);
      }
    })
  }

  //Go to register page.
  navigateToRegister() {
    this.router.navigate(['register'])
  }

}
