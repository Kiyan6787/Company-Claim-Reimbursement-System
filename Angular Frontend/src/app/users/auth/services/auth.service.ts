import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  baseHttpsUrl: string = 'https://localhost:XXXX'
  baseHttpUrl: string = 'https://localhost:XXXX'

  constructor(private http: HttpClient, private router: Router) { }

  //API call for registering a user.
  register(data: any): Observable<any> {
    console.log("Registration Data: ", data);
    return this.http.post(`${this.baseHttpsUrl}/api/Auth/register`, data);
  }

  //API call for login.
  login(data: any): Observable<any> {
    return this.http.post(`${this.baseHttpsUrl}/api/Auth/login`, data);
  }


  //API call for login. Currently using this one.
  loginUser(data: any): Observable<any> {
    return this.http.post(`${this.baseHttpsUrl}/api/Auth/userLogin`, data);
  }

  //Gets list of banks.
  getBanks(): Observable<any[]> {
    return this.http.get<any[]>(`${this.baseHttpsUrl}/api/Auth/getBanks`);
  }

  //Gets the token stored in local storage after login.
  getToken(): string | null {
    return localStorage.getItem('auth_token');
  }

  //Clears local storage and navigates to login.
  logoutUser() {
    localStorage.clear();
    this.router.navigate(['']);
  }

  //Checks if a user is logged in by checking local storage for a token.
  isLoggedIn(): boolean {
    return !!localStorage.getItem('token');
  }

}
