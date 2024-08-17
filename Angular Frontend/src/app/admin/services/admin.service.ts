import { HttpHeaders } from '@angular/common/http';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  private claimUpdatedSource = new BehaviorSubject<boolean>(false);
  claimUpdated$ = this.claimUpdatedSource.asObservable();
  baseHttpsUrl: string = 'https://localhost:XXXX'
  baseHttpUrl: string = 'https://localhost:XXXX'

  constructor(private http: HttpClient) { }

  //API call for getting all pending claims.
  getAllClaims(): Observable<any[]> {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.get<any[]>(`${this.baseHttpsUrl}/api/Admin/pendingClaims`, { headers });
  }

  //API call for getting all claims.
  getClaims(): Observable<any[]> {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.get<any[]>(`${this.baseHttpsUrl}/api/Admin/allClaims`, { headers });
  }

  //API call for approving claims.
  approveClaim(data: any, id: any): Observable<any> {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.put(`${this.baseHttpsUrl}/api/Admin/approveClaim?id=${id}`, data, { headers })
  }

  //API call for declining claims.
  declineClaim(id: any): Observable<any> {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.put(`${this.baseHttpsUrl}/api/Admin/declineClaim?id=${id}`, {}, { headers })
  }

  //API call for getting a claim.
  getClaim(id: any): Observable<any> {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.get(`${this.baseHttpsUrl}/api/Admin/getClaim?id=${id}`, { headers });
  }

  //API call for getting all approved claims.
  getApprovedClaims(): Observable<any> {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.get(`${this.baseHttpsUrl}/api/Admin/getApprovedClaims`, { headers });
  }

  //API call for getting all declined claims.
  getDeclinedClaims(): Observable<any> {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.get(`${this.baseHttpsUrl}/api/Admin/getDeclinedClaims`, { headers });
  }

  //API call for getting all types.
  getTypes(): Observable<any[]> {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.get<any[]>(`${this.baseHttpsUrl}/api/Reimbursement/reimbursementTypes`, { headers });
  }

  //API call for getting reimbursement types and associated amount.
  getTypesAndAmount(): Observable<any[]> {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.get<any[]>(`${this.baseHttpsUrl}/api/Admin/getTypesAndAmount`, { headers });
  }

  //API call for getting months and associated amount.
  getMonthsAndAmount(): Observable<any[]> {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.get<any[]>(`${this.baseHttpsUrl}/api/Admin/monthWiseReimbursements`, { headers });
  }

  notifyClaimUpdated() {
    this.claimUpdatedSource.next(true);
  }
}
