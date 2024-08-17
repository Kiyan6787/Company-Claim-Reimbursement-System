import { Routes } from '@angular/router';
import { RegisterComponent } from './users/auth/register/register.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { LoginComponent } from './users/auth/login/login.component';
import { ClaimsListComponent } from './claims/claims-list/claims-list.component';
import { CreateComponent } from './claims/create/create.component';
import { EditComponent } from './claims/edit/edit.component';
import { DashboardComponent } from './admin/dashboard/dashboard.component';
import { AccessDeniedComponent } from './access-denied/access-denied.component';
import { roleGuard } from './guard/role.guard';
import { loginGuard } from './guard/login.guard';
import { ApprvePopUpComponent } from './admin/claims/apprve-pop-up/apprve-pop-up.component';
import { ReportsComponent } from './admin/reports/reports.component';

export const routes: Routes = [
    {path:'', component: LoginComponent},
    {path:'register', component: RegisterComponent},
    {path:'claims', component: ClaimsListComponent, canActivate: [loginGuard]},
    {path:'createClaim', component: CreateComponent, canActivate: [loginGuard]},
    {path:'edit/:id', component: EditComponent, canActivate: [loginGuard]},
    {path:'dashboard', component: DashboardComponent, canActivate: [roleGuard, loginGuard]},
    {path:'reports', component: ReportsComponent, canActivate: [roleGuard, loginGuard]},
    {path:'access-denied', component: AccessDeniedComponent},
    {path:'**', component: NotFoundComponent}
];
