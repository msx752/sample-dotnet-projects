import { Injectable, Inject } from '@angular/core';
import { TokenStorageService } from '../services/token-storage.service';
import { CartApiService } from './api/cart-api.service';
import { CartDto } from '../models/responses/carts/cart.dto';
import { Observable, Observer } from 'rxjs';
import { ResponseModel } from '../models/responses/response-model';

@Injectable({
  providedIn: 'root'
})
export class SessionStateService {
  constructor(
    private tokenStorageService: TokenStorageService
    , private cartApiService: CartApiService
  ) { }

  public getCartId(): string {
    return this.tokenStorageService.getCartId();
  }

  public isLoggedIn(): boolean {
    var loginState = this.tokenStorageService.getUser() != null;
    if (!loginState) {
      this.clearSession();
    }
    return loginState;
  }

  public refreshUserCart(): Promise<any> {
    if (this.isLoggedIn()) {
      return this.cartApiService.getCart()
        .then((data) => {
          if (data.results.length > 0) {
            this.tokenStorageService.setCartId(data.results[0].id);
          } else {
            this.tokenStorageService.removeCartId();
          }
        })
        .catch((error) => {
          this.tokenStorageService.removeCartId();
        });
    }
    return new Promise((resolve, reject) => { });
  }

  public clearSession(): void {
    this.tokenStorageService.removeCartId();
  }
}
