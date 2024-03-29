import { Injectable, Inject } from '@angular/core';
import { UserDto } from '../models/responses/identity/user.dto';

const TOKEN_KEY = 'auth-token';
const USER_KEY = 'auth-user';
const CARTID_KEY = 'cardid-user'

@Injectable({
  providedIn: 'root'
})
export class TokenStorageService {
  constructor() { }

  public saveToken(token: string): void {
    window.sessionStorage.removeItem(TOKEN_KEY);
    window.sessionStorage.setItem(TOKEN_KEY, token);
  }

  public getToken(): string | null {
    return window.sessionStorage.getItem(TOKEN_KEY);
  }

  public saveUser(user: any): void {
    window.sessionStorage.removeItem(USER_KEY);
    window.sessionStorage.setItem(USER_KEY, JSON.stringify(user));
  }

  public logout(reloadPage: boolean = false) {
    window.sessionStorage.clear();

    if (reloadPage) {
      location.reload();
    }
  }

  public isLoggedIn(): boolean {
    return this.getUser() != null;
  }

  public getUser(): UserDto {
    const user = window.sessionStorage.getItem(USER_KEY);
    if (user) {
      return JSON.parse(user);
    }

    return null;
  }

  public setCartId(cardid: string): void {
    window.sessionStorage.removeItem(CARTID_KEY);
    window.sessionStorage.setItem(CARTID_KEY, cardid);
  }

  public getCartId(): string {
    return window.sessionStorage.getItem(CARTID_KEY);
  }

  public removeCartId(): void {
    window.sessionStorage.removeItem(CARTID_KEY);
  }
}
