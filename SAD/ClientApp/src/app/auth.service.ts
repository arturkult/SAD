import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private jwtHelperService: JwtHelperService = new JwtHelperService();

  public get isLoggedIn() {
    const token = sessionStorage.getItem("jwt");
    return !!token && !this.jwtHelperService.isTokenExpired(token);
  }
  private readonly url = "api/user";
  constructor(
    private http: HttpClient,
    private router: Router) {
  }

  public login(user) {
    return this.http.post(`${this.url}/login`, user)
      .subscribe(
        (result: { token: string } ) => {
          console.log(result);
          sessionStorage.setItem("jwt", result.token);
          this.router.navigateByUrl("home");
        },
        (error) => {
          console.error(error);
        });
  }

  public logout() {
    sessionStorage.removeItem("jwt");
  }
}
