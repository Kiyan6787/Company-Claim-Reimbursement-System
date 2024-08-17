import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ClaimsService {

  baseHttpsUrl: string = 'https://localhost:XXXX'
  baseHttpUrl: string = 'https://localhost:XXXX'

  constructor(private http: HttpClient) { }

  //API call to get all claims.
  getClaims(): Observable<any[]> {
    const token = localStorage.getItem('token');
    const email: string | null = localStorage.getItem('email');
    const safeEmail = email ? encodeURIComponent(email) : '';
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.get<any[]>(`${this.baseHttpsUrl}/api/Reimbursement/claims?useremail=${safeEmail}`, { headers });
  }

  //API call to create a claim.
  createClaims(data: any): Observable<any> {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.post(`${this.baseHttpsUrl}/api/Reimbursement/createClaim`, data, { headers })
  }

  //API call to delete a claim.
  deleteClaim(id: any): Observable<any> {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.delete(`${this.baseHttpsUrl}/api/Reimbursement/deleteClaim?id=${id}`, { headers })
  }

  //API call to update claims.
  updateClaim(data: any, id: any): Observable<any> {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.patch(`${this.baseHttpsUrl}/api/Reimbursement/updateClaim?id=${id}`, data, { headers })
  }

  //API call to get a claim.
  getClaim(id: any): Observable<any> {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.get(`${this.baseHttpsUrl}/api/Reimbursement/getClaim?id=${id}`, { headers });
  }

  //API call to get all currencies.
  getCurrencies(): Observable<any[]> {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.get<any[]>(`${this.baseHttpsUrl}/api/Reimbursement/currencies`, { headers });
  }

  //API call to get all reimbursement types.
  getTypes(): Observable<any[]> {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.get<any[]>(`${this.baseHttpsUrl}/api/Reimbursement/reimbursementTypes`, { headers });
  }
}
