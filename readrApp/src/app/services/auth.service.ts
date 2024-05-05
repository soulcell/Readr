import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, shareReplay, tap } from 'rxjs';
import API from 'src/consts/api-endpoints';
import { AUTH_TOKEN } from 'src/consts/local-storage';
import User from 'src/models/user';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient) { }

  user: User = {}

  getSecurityCode(phone: string) {
    return this.http.post<string>(API.AUTH.GET_SECURITY_CODE, { phone: phone }).pipe(
      catchError(async err => console.log(err)),
      tap(res => this.user.phone = phone),
      shareReplay(),
    );
  }

  checkSecurityCode(code: string) {
    return this.http.post(API.AUTH.CHECK_SECURITY_CODE, { phone: this.user.phone, code: code }, { responseType: 'text' }).pipe(
      tap(res => this.setToken(res)), 
      shareReplay()
    )
  }

  logout() {
    this.unsetToken();
  }

  setUserName(userName: string) {
    return this.http.post(API.AUTH.SET_USER_NAME, {name: userName}).pipe(
      tap({next: _ => this.user.name = userName}),
      shareReplay()
    )
  }

  getToken() {
    return localStorage.getItem(AUTH_TOKEN) || '';
  }

  private setToken(token: string) {
    localStorage.setItem(AUTH_TOKEN, token);
  }

  private unsetToken() {
    localStorage.removeItem(AUTH_TOKEN);
  }
}
