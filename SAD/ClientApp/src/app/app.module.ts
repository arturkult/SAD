import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { MatPaginatorModule } from '@angular/material/paginator';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { LoginFormComponent } from './login-form/login-form.component';
import { AuthService } from './auth.service';
import { JwtHelperService } from '@auth0/angular-jwt';
import { AuthGuardService } from './auth-guard.service';
import { UserListComponent } from './user-list/user-list.component';
import { UserFormComponent } from './user-form/user-form.component';
import { UserService } from './user.service';
import { AuditLogListComponent } from './audit-log-list/audit-log-list.component';
import { AuditLogLiveComponent } from './audit-log-live/audit-log-live.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    LoginFormComponent,
    UserListComponent,
    UserFormComponent,
    AuditLogListComponent,
    AuditLogLiveComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    MatPaginatorModule,
    BrowserAnimationsModule,
    RouterModule.forRoot([
      { path: 'login', component: LoginFormComponent },
      { path: 'home', component: HomeComponent, canActivate: [AuthGuardService] },
      { path: 'users', component: UserListComponent, canActivate: [AuthGuardService] },
      { path: 'users/form', component: UserFormComponent, canActivate: [AuthGuardService] },
      { path: 'users/form/:id', component: UserFormComponent, canActivate: [AuthGuardService] },
      { path: 'audit-logs/list', component: AuditLogListComponent, canActivate: [AuthGuardService] },
      { path: 'audit-logs/live', component: AuditLogLiveComponent, canActivate: [AuthGuardService] },
      { path: '*', redirectTo: 'home' }
    ])
  ],
  providers: [
    AuthService,
    JwtHelperService,
    AuthGuardService,
    UserService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
